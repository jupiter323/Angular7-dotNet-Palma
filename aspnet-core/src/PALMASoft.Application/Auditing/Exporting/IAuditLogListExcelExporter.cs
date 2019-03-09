using System.Collections.Generic;
using PALMASoft.Auditing.Dto;
using PALMASoft.Dto;

namespace PALMASoft.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
