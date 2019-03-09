using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PALMASoft.Departamentos.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.Departamentos
{
    public interface IDepartamentosAppService : IApplicationService 
    {
        Task<PagedResultDto<GetDepartamentoForViewDto>> GetAll(GetAllDepartamentosInput input);

        Task<GetDepartamentoForViewDto> GetDepartamentoForView(long id);

		Task<GetDepartamentoForEditOutput> GetDepartamentoForEdit(EntityDto<long> input);

		Task CreateOrEdit(CreateOrEditDepartamentoDto input);

		Task Delete(EntityDto<long> input);

		Task<FileDto> GetDepartamentosToExcel(GetAllDepartamentosForExcelInput input);

		
		Task<PagedResultDto<PaisLookupTableDto>> GetAllPaisForLookupTable(GetAllForLookupTableInput input);
		
    }
}