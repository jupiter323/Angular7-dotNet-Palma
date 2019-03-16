using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PALMASoft.AFoliares.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.AFoliares
{
    public interface IAFoliaresAppService : IApplicationService 
    {
        Task<PagedResultDto<GetAFoliarForViewDto>> GetAll(GetAllAFoliaresInput input);

        Task<GetAFoliarForViewDto> GetAFoliarForView(long id);

		Task<GetAFoliarForEditOutput> GetAFoliarForEdit(EntityDto<long> input);

		Task CreateOrEdit(CreateOrEditAFoliarDto input);

		Task Delete(EntityDto<long> input);

		Task<FileDto> GetAFoliaresToExcel(GetAllAFoliaresForExcelInput input);

		
    }
}