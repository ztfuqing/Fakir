using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Fakir.Metting.Domain;

namespace Fakir.Metting.Dtos
{
    [AutoMap(typeof(ConferenceUser))]
    public class ConferenceUserDto : IDto
    {
        public long UserId { get; set; }

        public string DisplayName { get; set; }
    }
}