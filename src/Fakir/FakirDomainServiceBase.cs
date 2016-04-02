using Abp.Domain.Services;

namespace Fakir
{
    public abstract class FakirDomainServiceBase : DomainService
    {
        protected FakirDomainServiceBase()
        {
            LocalizationSourceName = FakirConsts.LocalizationSourceName;
        }
    }
}
