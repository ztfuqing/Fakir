using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakir.Bpm.Model
{
    /// <summary>
    /// 流程实例
    /// </summary>
    public class ProcessInstance
    {
        public Workflow Workflow { get; set; }

        public bool IsFinish { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public ProcessInstance ParentProcessInstance { get; set; }

        public Dictionary<string, object> Datas { get; set; }

        public string Name { get; set; }

        public DateTime? UpdateTime { get; set; }

        public DateTime? ArchiveTime { get; set; }

        public string StartAuthor { get; set; }

        public ProcessInstanceStatus Status { get; set; }





    }
}
