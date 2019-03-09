using System.Collections.Generic;
using PALMASoft.Chat.Dto;
using PALMASoft.Dto;

namespace PALMASoft.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(List<ChatMessageExportDto> messages);
    }
}
