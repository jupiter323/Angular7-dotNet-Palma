using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PALMASoft.Paises.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.Paises
{
    public interface IPaisesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetPaisForViewDto>> GetAll(GetAllPaisesInput input);

        Task<GetPaisForViewDto> GetPaisForView(long id);

		Task<GetPaisForEditOutput> GetPaisForEdit(EntityDto<long> input);

		Task CreateOrEdit(CreateOrEditPaisDto input);

		Task Delete(EntityDto<long> input);

		Task<FileDto> GetPaisesToExcel(GetAllPaisesForExcelInput input);

		
    }
}