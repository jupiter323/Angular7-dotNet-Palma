using System.Collections.Generic;
using PALMASoft.Departamentos.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.Departamentos.Exporting
{
    public interface IDepartamentosExcelExporter
    {
        FileDto ExportToFile(List<GetDepartamentoForViewDto> departamentos);
    }
}