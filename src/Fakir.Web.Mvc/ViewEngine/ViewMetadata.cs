using System;

namespace Fakir.Web.Mvc.ViewEngine
{


    [Serializable]
    public class ViewMetaData
    {
        public string Name { get; set; }
        public string AssemblyFullName { get; set; }
    }
}
