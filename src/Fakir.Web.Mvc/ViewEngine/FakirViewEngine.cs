using System.Web.Mvc;

namespace Fakir.Web.Mvc.ViewEngine
{
    public class FakirViewEngine : BuildManagerViewEngine
    {
        public FakirViewEngine()
            : this(null)
        {
        }

        public FakirViewEngine(IViewPageActivator viewPageActivator)
            : base(viewPageActivator)
        {
            ViewLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"

            };
            MasterLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };
            PartialViewLocationFormats = new[] {
                "~/Views/{1}/{0}.cshtml",
                "~/Views/Shared/{0}.cshtml"
            };

            AreaViewLocationFormats = ViewLocationFormats;
            AreaMasterLocationFormats = MasterLocationFormats;
            AreaPartialViewLocationFormats = PartialViewLocationFormats;

            FileExtensions = new[] {
                "cshtml"
            };
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            return new RazorView(controllerContext, partialPath, null, false, FileExtensions, ViewPageActivator);
        }

        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var view = new RazorView(controllerContext, viewPath, masterPath, true, FileExtensions, ViewPageActivator);

            return view;
        }
    }
}