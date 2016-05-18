using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Abp.Web.Mvc.Views;

namespace Fakir.Web.Mvc
{
    public abstract class FakirWebViewPage : FakirWebViewPage<dynamic>
    {

    }

    public abstract class FakirWebViewPage<TModel> : AbpWebViewPage<TModel>
    {
        protected FakirWebViewPage()
        {
            LocalizationSourceName = FakirConsts.LocalizationSourceName;
        }

        public override string Layout
        {
            get
            {
                var layout = base.Layout;

                if (!string.IsNullOrEmpty(layout))
                {
                    var filename = Path.GetFileNameWithoutExtension(layout);
                    ViewEngineResult viewResult = ViewEngines.Engines.FindView(ViewContext.Controller.ControllerContext, filename, "");

                    if (viewResult.View != null && viewResult.View is RazorView)
                    {
                        layout = (viewResult.View as RazorView).ViewPath;
                    }
                }

                return layout;
            }
            set
            {
                base.Layout = value;
            }
        }
    }
}
