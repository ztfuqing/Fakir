using System.Collections.Generic;
using Fakir.Admin.Dtos.AuditLogs;
using Fakir.Dto;

namespace Fakir.Admin.Services.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);
    }
}
