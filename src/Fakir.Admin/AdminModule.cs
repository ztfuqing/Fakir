using Abp.Modules;
using System.Reflection;

namespace Fakir.Admin
{
    [DependsOn(typeof(FakirModule))]
    public class AdminModule : AbpModule
    {
        public override void PreInitialize()
        {

        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}
