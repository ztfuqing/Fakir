using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakir.Bpm.Model
{
    /// <summary>
    /// 流程步骤
    /// </summary>
    public class WorkflowStep
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public Guid WorkflowId { get; set; }

        public Workflow Workflow { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public WorkflowStepType StepType { get; set; }

        public AuditWay AuditWay { get; set; }

        public int StepNumber { get; set; }

        public Dictionary<string, object> Datas { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreateUser { get; set; }
    }
}
