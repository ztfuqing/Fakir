using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakir.Bpm.Services
{
    public interface IWorkflowEngine
    {
        string Version { get; }

        string Name { get; }

        IFormService FormService { get; set; }

        IScheduleService ScheduleService { get; set; }

        IRuntimeService RuntimeService { get; set; }

        ITaskService TaskService { get; set; }

        IHistoryService HistoryService { get; set; }

        void Close();
    }
}
