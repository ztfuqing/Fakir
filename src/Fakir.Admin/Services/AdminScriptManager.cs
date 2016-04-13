using Abp.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakir.Admin.Services
{
    public class AdminScriptManager : FakirServiceBase, IAdminScriptManager, ITransientDependency
    {
        public string GetMainScript()
        {
            throw new NotImplementedException();
        }

        public string GetRouterScript()
        {
            throw new NotImplementedException();
        }
    }
}
