using Abp.Modules;
using Abp.Zero.EntityFramework;
using System.Reflection;

namespace Fakir.EntityFramework
{
    [DependsOn(typeof(AbpZeroEntityFrameworkModule), typeof(FakirModule))]
    public class FakirDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
