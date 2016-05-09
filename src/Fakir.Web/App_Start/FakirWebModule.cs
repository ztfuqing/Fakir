using Abp.Hangfire;
using Abp.IO;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Abp.Zero.Configuration;
using Castle.MicroKernel.Registration;
using Fakir.EntityFramework;
using Microsoft.Owin.Security;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Fakir.Web
{
    [DependsOn(
    typeof(AbpWebMvcModule),
    typeof(FakirDataModule),
    typeof(FakirModule),
    typeof(AbpWebSignalRModule),
    typeof(AbpHangfireModule))]
    public class FakirWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Use database as language management
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            //Configure navigation/menu
            //Configuration.Navigation.Providers.Add<AppNavigationProvider>();
            //Configuration.Navigation.Providers.Add<FrontEndNavigationProvider>();
            //Configuration.Navigation.Providers.Add<MpaNavigationProvider>();

            //Configure to use Hangfire as background job manager. Remove these lines to use default background job manager, instead of Hangfire.
            //Configuration.BackgroundJobs.UseHangfire(configuration =>
            //{
            //    configuration.GlobalConfiguration.UseMySqlStorage("Default");
            //});
        }

        public override void Initialize()
        {
            //Dependency Injection
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.IocContainer.Register(
                Component
                    .For<IAuthenticationManager>()
                    .UsingFactoryMethod(() => HttpContext.Current.GetOwinContext().Authentication)
                    .LifestyleTransient()
                );

            //Areas
            AreaRegistration.RegisterAllAreas();
            AdminBundleConfig.RegisterBundles(BundleTable.Bundles);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        public override void PostInitialize()
        {
            var server = HttpContext.Current.Server;
            var appFolders = IocManager.Resolve<AppFolders>();

            appFolders.SampleProfileImagesFolder = server.MapPath("~/Common/Images/SampleProfilePics");
            appFolders.TempFileDownloadFolder = server.MapPath("~/Temp/Downloads");

            try { DirectoryHelper.CreateIfNotExists(appFolders.TempFileDownloadFolder); } catch { }
        }
    }
}