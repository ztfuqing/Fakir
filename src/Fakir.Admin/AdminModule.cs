using Abp.Modules;
using System.Reflection;
using Abp.Application.Services;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace Fakir.Admin
{
    [DependsOn(typeof(FakirModule))]
    public class AdminModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<AdminNavigationProvider>();
            Configuration.Authorization.Providers.Add<AdminAuthorizationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(AdminModule).Assembly, "app")
                .Build();
        }
    }
}
