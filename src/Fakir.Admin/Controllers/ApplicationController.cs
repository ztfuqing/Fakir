using Abp.Web.Mvc.Controllers;
using System.Web.Mvc;

namespace Fakir.Admin.Controllers
{
    public class ApplicationController : AbpController
    {
        public ApplicationController()
        {

        }

        public ActionResult Index()
        {
            return View("~/Modules/Admin/index.cshtml");
        }
    }
}
