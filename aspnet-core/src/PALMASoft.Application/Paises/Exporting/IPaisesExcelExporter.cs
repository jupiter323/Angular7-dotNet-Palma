using System.Collections.Generic;
using PALMASoft.Paises.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.Paises.Exporting
{
    public interface IPaisesExcelExporter
    {
        FileDto ExportToFile(List<GetPaisForViewDto> paises);
    }
}