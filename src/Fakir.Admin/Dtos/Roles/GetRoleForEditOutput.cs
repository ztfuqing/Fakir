using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Fakir.Dto;

namespace Fakir.Admin.Dtos.Roles
{
    public class GetRoleForEditOutput : IOutputDto
    {
        public RoleEditDto Role { get; set; }

        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}