using Abp.Auditing;
using Abp.Extensions;
using Abp.Web.Mvc.Controllers;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Fakir.Admin.Controllers
{
    public class AdminScriptController : AbpController
    {
        public AdminScriptController()
        {

        }

        [DisableAuditing]
        public async Task<ActionResult> GetScripts(string culture = "")
        {
            if (!culture.IsNullOrEmpty())
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(culture);
            }

            StringBuilder sb = new StringBuilder();

            return Content(sb.ToString(), "application/x-javascript", Encoding.UTF8);
        }
    }
}
