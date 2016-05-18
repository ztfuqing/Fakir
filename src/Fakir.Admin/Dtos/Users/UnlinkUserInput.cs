using Abp.Application.Services.Dto;

namespace Fakir.Admin.Dtos.Users
{
    public class UnlinkUserInput : IInputDto
    {
        public long UserId { get; set; }
    }
}