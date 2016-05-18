using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Fakir.Dto;

namespace Fakir.Admin.Dtos.Users
{
    public class GetUserPermissionsForEditOutput : IOutputDto
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}