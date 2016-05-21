
using Abp.Modules;
using System.Reflection;
using Abp.Application.Services;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace Fakir.Metting
{
    [DependsOn(typeof(FakirModule))]
    public class MeetingModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<MeetingNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            CustomDtoMapper.CreateMappings();

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(MeetingModule).Assembly, "meet")
                .Build();
        }
    }
}
