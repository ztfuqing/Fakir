using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Fakir.Authorization.Users;

namespace Fakir.Metting.Domain
{
    [Table("m_agenda_file")]
    public class AgendaFile : CreationAuditedEntity<int, User>
    {
        [ForeignKey("AgendaId")]
        public virtual Agenda Agenda { get; set; }

        public virtual int AgendaId { get; set; }

        [StringLength(50)]
        public virtual string FileName { get; set; }

        [StringLength(200)]
        public virtual string FileUrl { get; set; }

        public virtual int FileSize { get; set; }

        [StringLength(20)]
        public virtual string FileType { get; set; }
    }
}
