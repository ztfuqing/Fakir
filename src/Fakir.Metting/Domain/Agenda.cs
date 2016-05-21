using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace Fakir.Metting.Domain
{
    /// <summary>
    /// 议程
    /// </summary>
    [Table("m_agenda")]
    public class Agenda:Entity<int>
    {
        [ForeignKey("ConferenceId")]
        public virtual Conference Conference { get; set; }

        public virtual int ConferenceId { get; set; }

        //议程名称
        [StringLength(100)]
        public virtual string Subject { get; set; }

        //发言人
        [StringLength(50)]
        public virtual string Spokesman { get; set; }

        public virtual DateTime SpeakTime { get; set; }

        [StringLength(50)]
        public virtual string SpeakLength { get; set; }

        [StringLength(100)]
        public virtual string DepartmentName { get; set; }

        public virtual long DepartmentId { get; set; }

        public virtual int Order { get; set; }

        [MaxLength]
        public virtual string Content { get; set; }

        public virtual ICollection<AgendaFile> Files { get; set; }
    }
}
