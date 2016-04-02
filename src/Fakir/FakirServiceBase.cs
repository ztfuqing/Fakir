using Abp;

namespace Fakir
{
    public abstract class FakirServiceBase : AbpServiceBase
    {
        protected FakirServiceBase()
        {
            LocalizationSourceName = FakirConsts.LocalizationSourceName;
        }
    }
}
