using System;
using System.IO;
using System.Reflection;
using System.Web.Hosting;
using Abp.Dependency;
using Abp.Runtime.Caching;

namespace Fakir.Web.Mvc.ViewEngine
{
    public class FakirResourceVirtualFile : VirtualFile
    {
        private readonly ViewMetaData _embeddedViewMetadata;

        public FakirResourceVirtualFile(ViewMetaData embeddedViewMetadata, string virtualPath)
            : base(virtualPath)
        {
            if (embeddedViewMetadata == null)
                throw new ArgumentNullException("embeddedViewMetadata");

            this._embeddedViewMetadata = embeddedViewMetadata;
        }

        public override Stream Open()
        {
            var cashManager = IocManager.Instance.Resolve<ICacheManager>();
            var cash = cashManager.GetCache("viewCash");

            Assembly assembly = cash.GetOrDefault(_embeddedViewMetadata.AssemblyFullName) as Assembly;

            return assembly == null ? null : assembly.GetManifestResourceStream(_embeddedViewMetadata.Name);
        }
    }
}
