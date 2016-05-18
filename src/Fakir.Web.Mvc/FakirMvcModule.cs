using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using Abp.Modules;
using Abp.Web.Mvc;
using Fakir.Web.Mvc.ViewEngine;

namespace Fakir.Web.Mvc
{
    [DependsOn(typeof(AbpWebMvcModule))]
    public class FakirMvcModule : AbpModule
    {
        public override void PreInitialize()
        {
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            //if (HostingEnvironment.IsHosted)
            //{
            //    var easymanProvider = new FakirViewVirtualPathProvider();
            //    HostingEnvironment.RegisterVirtualPathProvider(easymanProvider);
            //}



            //ViewEngines.Engines.Clear();
            //ViewEngines.Engines.Add(new FakirViewEngine());
        }
    }
}
