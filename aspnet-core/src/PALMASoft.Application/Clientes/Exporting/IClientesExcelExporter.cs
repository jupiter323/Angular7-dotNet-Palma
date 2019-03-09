using System.Collections.Generic;
using PALMASoft.Clientes.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.Clientes.Exporting
{
    public interface IClientesExcelExporter
    {
        FileDto ExportToFile(List<GetClienteForViewDto> clientes);
    }
}