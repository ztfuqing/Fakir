using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Fakir.Dto;

namespace Fakir.Admin.Dtos.Users
{
    public class GetDepartmentUserTreeOutput : IDto
    {
        public List<FlatDepartmentUserDto> DepartmentUsers { get; set; }

        public List<string> SelectedUsers { get; set; }
    }
}
