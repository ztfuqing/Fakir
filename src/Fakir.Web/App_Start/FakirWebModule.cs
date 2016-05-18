using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Hangfire;
using Abp.IO;
using Abp.Modules;
using Abp.Web.Mvc;
using Abp.Web.SignalR;
using Abp.Zero.Configuration;
using Castle.MicroKernel.Registration;
using Fakir.EntityFramework;
using Fakir.Web.Admin;
using Fakir.Web.Bundling;
using Microsoft.Owin.Security;

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
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Navigation.Providers.Add<FrontEndNavigationProvider>();
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
            //AppBundleConfig.RegisterBundles(BundleTable.Bundles);
            AdminBundleConfig.RegisterBundles(BundleTable.Bundles);
            FrontEndBundleConfig.RegisterBundles(BundleTable.Bundles);
            CommonBundleConfig.RegisterBundles(BundleTable.Bundles);
            BundleTable.EnableOptimizations = true;
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