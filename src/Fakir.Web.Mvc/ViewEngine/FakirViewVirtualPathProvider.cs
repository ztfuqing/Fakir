using System;
using System.Collections;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;
using Abp.Dependency;
using Abp.Runtime.Caching;

namespace Fakir.Web.Mvc.ViewEngine
{
    public class FakirViewVirtualPathProvider : VirtualPathProvider
    {
        public override bool FileExists(string virtualPath)
        {
            return base.FileExists(virtualPath) || IsEmbeddedView(virtualPath);
        }

        private bool IsEmbeddedView(string virtualPath)
        {
            if (string.IsNullOrEmpty(virtualPath))
                return false;

            string virtualPathAppRelative = VirtualPathUtility.ToAppRelative(virtualPath);
            if (!virtualPathAppRelative.StartsWith("~/Views/", StringComparison.InvariantCultureIgnoreCase))
                return false;
            var fullyQualifiedViewName = virtualPathAppRelative.Substring(virtualPathAppRelative.LastIndexOf("/", System.StringComparison.Ordinal) + 1, virtualPathAppRelative.Length - 1 - virtualPathAppRelative.LastIndexOf("/", System.StringComparison.Ordinal));
            var result = GetViewMetadata(fullyQualifiedViewName);

            return result != null;
        }

        public override VirtualFile GetFile(string virtualPath)
        {
            if (IsEmbeddedView(virtualPath))
            {
                string virtualPathAppRelative = VirtualPathUtility.ToAppRelative(virtualPath);
                var fullyQualifiedViewName = virtualPathAppRelative.Substring(virtualPathAppRelative.LastIndexOf("/", System.StringComparison.Ordinal) + 1, virtualPathAppRelative.Length - 1 - virtualPathAppRelative.LastIndexOf("/", System.StringComparison.Ordinal));

                return new FakirResourceVirtualFile(GetViewMetadata(fullyQualifiedViewName), virtualPath);
            }

            return Previous.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(
            string virtualPath,
            IEnumerable virtualPathDependencies,
            DateTime utcStart)
        {
            return IsEmbeddedView(virtualPath)
                ? null : Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }

        private ViewMetaData GetViewMetadata(string fullViewName)
        {
            var index = fullViewName.ToLower().IndexOf(".views", System.StringComparison.Ordinal);

            if (index == -1)
                return null;

            var assemblyName = fullViewName.Substring(0, index).ToLower();
            var assembly = GetAssembly(assemblyName);
            var stream = assembly.GetManifestResourceStream(fullViewName);

            if (stream != null)
                return new ViewMetaData()
                {
                    Name = fullViewName,
                    AssemblyFullName = assemblyName
                };
            else
                return null;
        }

        private Assembly GetAssembly(string assemblyName)
        {
            var cashManager = IocManager.Instance.Resolve<ICacheManager>();
            var cash = cashManager.GetCache("viewCash");
            var assembly = cash.GetOrDefault(assemblyName) as Assembly;

            if (assembly != null) return assembly;

            assembly = Assembly.Load(assemblyName);
            cash.Set(assemblyName, assembly);

            return assembly;
        }
    }
}
