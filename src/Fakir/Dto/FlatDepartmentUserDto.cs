using Abp.Application.Services.Dto;

namespace Fakir.Dto
{
    public class FlatDepartmentUserDto : IDto
    {
        public string ParentId { get; set; }

        public string Id { get; set; }

        public string DisplayName { get; set; }
    }
}
