using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PALMASoft.Fincas.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.Fincas
{
    public interface IFincasAppService : IApplicationService 
    {
        Task<PagedResultDto<GetFincaForViewDto>> GetAll(GetAllFincasInput input);

        Task<GetFincaForViewDto> GetFincaForView(long id);

		Task<GetFincaForEditOutput> GetFincaForEdit(EntityDto<long> input);

		Task CreateOrEdit(CreateOrEditFincaDto input);

		Task Delete(EntityDto<long> input);

		Task<FileDto> GetFincasToExcel(GetAllFincasForExcelInput input);

		
		Task<PagedResultDto<ClienteLookupTableDto>> GetAllClienteForLookupTable(GetAllForLookupTableInput input);
		
    }
}