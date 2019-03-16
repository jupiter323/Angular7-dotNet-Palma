using System.Collections.Generic;
using PALMASoft.Analises.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.Analises.Exporting
{
    public interface IAnalisesExcelExporter
    {
        FileDto ExportToFile(List<GetAnalisisForViewDto> analises);
    }
}