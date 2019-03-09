using System.Collections.Generic;
using PALMASoft.Municipios.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.Municipios.Exporting
{
    public interface IMunicipiosExcelExporter
    {
        FileDto ExportToFile(List<GetMunicipioForViewDto> municipios);
    }
}