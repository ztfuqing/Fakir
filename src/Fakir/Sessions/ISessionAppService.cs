using System.Threading.Tasks;
using Abp.Application.Services;
using Fakir.Sessions.Dto;

namespace Fakir.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
