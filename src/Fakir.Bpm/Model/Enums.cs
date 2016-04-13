using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakir.Bpm.Model
{
    /// <summary>
    /// 步骤类型，开始、常规、结束等
    /// </summary>
    public enum WorkflowStepType
    {
        Normal = 0
    }

    /// <summary>
    /// 步骤审核类型，所有人、任一人
    /// </summary>
    public enum AuditWay
    {
        All,
        EveryOne
    }

    /// <summary>
    /// 流程实例状态
    /// </summary>
    public enum ProcessInstanceStatus
    {
        Running,
        Suspend,
        Complete,
        Termination
    }
}
