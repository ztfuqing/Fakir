using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Fakir.Authorization.Users;

namespace Fakir.Metting.Domain
{
    [Table("m_conference_user")]
    public class ConferenceUser : CreationAuditedEntity<int, User>
    {
        [ForeignKey("ConferenceId")]
        public virtual Conference Conference { get; set; }

        public virtual int ConferenceId { get; set; }
    }
}
