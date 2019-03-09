using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PALMASoft.Municipios.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.Municipios
{
    public interface IMunicipiosAppService : IApplicationService 
    {
        Task<PagedResultDto<GetMunicipioForViewDto>> GetAll(GetAllMunicipiosInput input);

        Task<GetMunicipioForViewDto> GetMunicipioForView(long id);

		Task<GetMunicipioForEditOutput> GetMunicipioForEdit(EntityDto<long> input);

		Task CreateOrEdit(CreateOrEditMunicipioDto input);

		Task Delete(EntityDto<long> input);

		Task<FileDto> GetMunicipiosToExcel(GetAllMunicipiosForExcelInput input);

		
		Task<PagedResultDto<DepartamentoLookupTableDto>> GetAllDepartamentoForLookupTable(GetAllForLookupTableInput input);
		
    }
}