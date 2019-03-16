using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PALMASoft.Analises.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.Analises
{
    public interface IAnalisesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetAnalisisForViewDto>> GetAll(GetAllAnalisesInput input);

        Task<GetAnalisisForViewDto> GetAnalisisForView(long id);

		Task<GetAnalisisForEditOutput> GetAnalisisForEdit(EntityDto<long> input);

		Task CreateOrEdit(CreateOrEditAnalisisDto input);

		Task Delete(EntityDto<long> input);

		Task<FileDto> GetAnalisesToExcel(GetAllAnalisesForExcelInput input);

		
		Task<PagedResultDto<FincaLookupTableDto>> GetAllFincaForLookupTable(GetAllForLookupTableInput input);
		
    }
}