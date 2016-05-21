using System.Reflection;
using System.Web.Http;
using System.Web.Http.Cors;
using Abp.Application.Services;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace Fakir.Web.Api
{
    [DependsOn(typeof(AbpWebApiModule), typeof(FakirModule))]
    public class FakirWebApiModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(FakirModule).Assembly, "app")
                .Build();

            Configuration.Modules
                .AbpWebApi()
                .HttpConfiguration
                .Filters.Add(new HostAuthenticationFilter("Bearer"));

            var cors = new EnableCorsAttribute("*", "*", "*");
            GlobalConfiguration.Configuration.EnableCors(cors);
        }
    }
}
