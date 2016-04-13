using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fakir.Bpm.Services
{
    /// <summary>
    /// 定时任务调度服务
    /// </summary>
    public interface IScheduleService
    {
        void Start();

        void Stop();
    }
}
