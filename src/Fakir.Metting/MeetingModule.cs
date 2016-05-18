
using Abp.Modules;
using System.Reflection;
using Abp.Application.Services;

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
        }
    }
}
