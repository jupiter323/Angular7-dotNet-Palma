using System.Collections.Generic;
using PALMASoft.AFoliares.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.AFoliares.Exporting
{
    public interface IAFoliaresExcelExporter
    {
        FileDto ExportToFile(List<GetAFoliarForViewDto> aFoliares);
    }
}