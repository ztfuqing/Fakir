using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Fakir.Authorization.Users;

namespace Fakir.Metting.Domain
{
    /// <summary>
    /// 会议
    /// </summary>
    [Table("m_conference")]
    public class Conference : CreationAuditedEntity<int, User>
    {
        //名称
        [StringLength(100)]
        public virtual string Name { get; set; }

        //主题
        [StringLength(100)]
        public virtual string Subject { get; set; }

        //目标
        [StringLength(1000)]
        public virtual string Target { get; set; }

        //开始时间
        public virtual DateTime StartTime { get; set; }

        //结束时间
        public virtual DateTime EndTime { get; set; }

        //地点
        [StringLength(100)]
        public virtual string Site { get; set; }

        //主持人
        [StringLength(50)]
        public virtual string Emcee { get; set; }

        //参会人员
        public virtual ICollection<ConferenceUser> Users { get; set; }


        //议程
        public virtual ICollection<Agenda> Agendas { get; set; }

        [StringLength(100)]
        public virtual string SummaryName { get; set; }

        public virtual string SummaryText { get; set; }

        public virtual bool IsActive { get; set; }
    }
}
