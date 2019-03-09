using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using PALMASoft.Clientes.Dtos;
using PALMASoft.Dto;

namespace PALMASoft.Clientes
{
    public interface IClientesAppService : IApplicationService 
    {
        Task<PagedResultDto<GetClienteForViewDto>> GetAll(GetAllClientesInput input);

        Task<GetClienteForViewDto> GetClienteForView(long id);

		Task<GetClienteForEditOutput> GetClienteForEdit(EntityDto<long> input);

		Task CreateOrEdit(CreateOrEditClienteDto input);

		Task Delete(EntityDto<long> input);

		Task<FileDto> GetClientesToExcel(GetAllClientesForExcelInput input);

		
    }
}