using System.Collections.Generic;
using PALMASoft.ASuelos.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.ASuelos.Exporting
{
    public interface IASuelosExcelExporter
    {
        FileDto ExportToFile(List<GetASueloForViewDto> aSuelos);
    }
}