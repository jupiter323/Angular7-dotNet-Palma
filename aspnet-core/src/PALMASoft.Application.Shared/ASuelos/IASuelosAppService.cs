using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PALMASoft.ASuelos.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.ASuelos
{
    public interface IASuelosAppService : IApplicationService 
    {
        Task<PagedResultDto<GetASueloForViewDto>> GetAll(GetAllASuelosInput input);

        Task<GetASueloForViewDto> GetASueloForView(long id);

		Task<GetASueloForEditOutput> GetASueloForEdit(EntityDto<long> input);

		Task CreateOrEdit(CreateOrEditASueloDto input);

		Task Delete(EntityDto<long> input);

		Task<FileDto> GetASuelosToExcel(GetAllASuelosForExcelInput input);

		
    }
}