using System.Collections.Generic;
using PALMASoft.Fincas.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.Fincas.Exporting
{
    public interface IFincasExcelExporter
    {
        FileDto ExportToFile(List<GetFincaForViewDto> fincas);
    }
}