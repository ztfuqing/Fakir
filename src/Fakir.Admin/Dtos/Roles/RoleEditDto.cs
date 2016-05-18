using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Fakir.Authorization.Roles;

namespace Fakir.Admin.Dtos.Roles
{
    [AutoMap(typeof(Role))]
    public class RoleEditDto
    {
        public int? Id { get; set; }

        [Required]
        public string DisplayName { get; set; }
        
        public bool IsDefault { get; set; }
    }
}