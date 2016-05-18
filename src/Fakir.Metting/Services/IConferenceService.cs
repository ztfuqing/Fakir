using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Fakir.Metting.Dtos;

namespace Fakir.Metting.Services
{
    public interface IConferenceService : IApplicationService
    {
        Task<PagedResultOutput<ConferenceListDto>> GetConferences(GetConferencesInput input);

        Task CreateOrUpdateConference(CreateOrUpdateConferenceInput input);

        Task<GetConferenceForEditOutput> GetConferenceForEdit(NullableIdInput<int> input);
    }
}
