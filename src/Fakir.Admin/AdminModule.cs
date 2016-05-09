using Abp.Modules;
using System.Reflection;

namespace Fakir.Admin
{
    [DependsOn(typeof(FakirModule))]
    public class AdminModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<AdminNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
