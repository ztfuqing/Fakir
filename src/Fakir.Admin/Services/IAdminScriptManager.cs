using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakir.Admin.Services
{
    public interface IAdminScriptManager
    {
        string GetMainScript();

        string GetRouterScript();
    }
}
