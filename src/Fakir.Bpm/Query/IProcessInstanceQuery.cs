using Fakir.Bpm.Model;
using System;
using System.Collections.Generic;

namespace Fakir.Bpm.Query
{
    public interface IProcessInstanceQuery
    {
        IProcessInstanceQuery ProcessInstanceId(string processInstanceId);


        IProcessInstanceQuery WorkflowId(string workflowId);

        IProcessInstanceQuery WorkflowId(List<string> workflowIdList);


        IProcessInstanceQuery Initiator(string initiator);

        IProcessInstanceQuery IsEnd();

        IProcessInstanceQuery ContainsSubProcess();

        IProcessInstanceQuery NotEnd();

        IProcessInstanceQuery StartTimeOn(DateTime startTime);

        IProcessInstanceQuery StartTimeBefore(DateTime startTimeBefore);

        IProcessInstanceQuery StartTimeAfter(DateTime startTimeAfter);

        IProcessInstanceQuery EndTimeOn(DateTime startTime);

        IProcessInstanceQuery EndTimeBefore(DateTime startTimeBefore);

        IProcessInstanceQuery EndTimeAfter(DateTime startTimeAfter);

        IProcessInstanceQuery ErchiveTimeOn(DateTime archiveTime);

        IProcessInstanceQuery ErchiveTimeBefore(DateTime archiveTimeBefore);

        IProcessInstanceQuery ErchiveTimeAfter(DateTime archiveTimeAfter);

        IProcessInstanceQuery SuperProcessInstanceId(string superProcessInstanceId);

        IProcessInstanceQuery SubProcessInstanceId(String subProcessInstanceId);

        IProcessInstanceQuery History();

        IProcessInstanceQuery Run();

        IProcessInstanceQuery ProcessInstanceStatus(ProcessInstanceStatus status);

        IProcessInstanceQuery OrderByProcessInstanceId();

        IProcessInstanceQuery OrderByStartTime();

        IProcessInstanceQuery OrderByUpdateTime();

        IProcessInstanceQuery OrderByEndTime();
    }
}
