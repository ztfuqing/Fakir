using System.Collections.Generic;

namespace Fakir.Bpm.Services
{
    /// <summary>
    /// 获取历史数据服务(主要作用是用于管理流程实例、任务实例、归档等历史数据的操作)
    /// </summary>
    public interface IHistoryService
    {
       bool ArchiveByProcessInstanceId(string processinstanceId);

        bool archiveByProcessInstanceIds(List<string> processinstanceId);

        bool ArchiveAll();
    }
}
