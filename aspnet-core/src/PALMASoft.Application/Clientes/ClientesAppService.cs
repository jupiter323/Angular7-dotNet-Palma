
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using PALMASoft.Clientes.Exporting;
using PALMASoft.Clientes.Dtos;
using PALMASoft.Dto;
using Abp.Application.Services.Dto;
using PALMASoft.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace PALMASoft.Clientes
{
	[AbpAuthorize(AppPermissions.Pages_Clientes)]
    public class ClientesAppService : PALMASoftAppServiceBase, IClientesAppService
    {
		 private readonly IRepository<Cliente, long> _clienteRepository;
		 private readonly IClientesExcelExporter _clientesExcelExporter;
		 

		  public ClientesAppService(IRepository<Cliente, long> clienteRepository, IClientesExcelExporter clientesExcelExporter ) 
		  {
			_clienteRepository = clienteRepository;
			_clientesExcelExporter = clientesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetClienteForViewDto>> GetAll(GetAllClientesInput input)
         {
			var generO_CLIENTEFilter = (Generos) input.GENERO_CLIENTEFilter;
			
			var filteredClientes = _clienteRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ID_CLIENTE.Contains(input.Filter) || e.NOMBRE_CLIENTE.Contains(input.Filter) || e.APELLIDO_CLIENTE.Contains(input.Filter) || e.CELULAR_CLIENTE.Contains(input.Filter) || e.DIRECCION_CLIENTE.Contains(input.Filter) || e.DEPARTAMENTO_CLIENTE.Contains(input.Filter) || e.MUNICIPIO_CLIENTE.Contains(input.Filter) || e.EMPRESA_CLIENTE.Contains(input.Filter) || e.PROFESION_CLIENTE.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_CLIENTEFilter),  e => e.ID_CLIENTE.ToLower() == input.ID_CLIENTEFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NOMBRE_CLIENTEFilter),  e => e.NOMBRE_CLIENTE.ToLower() == input.NOMBRE_CLIENTEFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.APELLIDO_CLIENTEFilter),  e => e.APELLIDO_CLIENTE.ToLower() == input.APELLIDO_CLIENTEFilter.ToLower().Trim())
						.WhereIf(input.GENERO_CLIENTEFilter > -1, e => e.GENERO_CLIENTE == generO_CLIENTEFilter)
						.WhereIf(input.MinFECHA_CLIENTEFilter != null, e => e.FECHA_CLIENTE >= input.MinFECHA_CLIENTEFilter)
						.WhereIf(input.MaxFECHA_CLIENTEFilter != null, e => e.FECHA_CLIENTE <= input.MaxFECHA_CLIENTEFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.CELULAR_CLIENTEFilter),  e => e.CELULAR_CLIENTE.ToLower() == input.CELULAR_CLIENTEFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DIRECCION_CLIENTEFilter),  e => e.DIRECCION_CLIENTE.ToLower() == input.DIRECCION_CLIENTEFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DEPARTAMENTO_CLIENTEFilter),  e => e.DEPARTAMENTO_CLIENTE.ToLower() == input.DEPARTAMENTO_CLIENTEFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.MUNICIPIO_CLIENTEFilter),  e => e.MUNICIPIO_CLIENTE.ToLower() == input.MUNICIPIO_CLIENTEFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.EMPRESA_CLIENTEFilter),  e => e.EMPRESA_CLIENTE.ToLower() == input.EMPRESA_CLIENTEFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PROFESION_CLIENTEFilter),  e => e.PROFESION_CLIENTE.ToLower() == input.PROFESION_CLIENTEFilter.ToLower().Trim());


			var query = (from o in filteredClientes
                         select new GetClienteForViewDto() {
							Cliente = ObjectMapper.Map<ClienteDto>(o)
						});

            var totalCount = await query.CountAsync();

            var clientes = await query
                .OrderBy(input.Sorting ?? "cliente.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetClienteForViewDto>(
                totalCount,
                clientes
            );
         }
		 
		 public async Task<GetClienteForViewDto> GetClienteForView(long id)
         {
            var cliente = await _clienteRepository.GetAsync(id);

            var output = new GetClienteForViewDto { Cliente = ObjectMapper.Map<ClienteDto>(cliente) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Clientes_Edit)]
		 public async Task<GetClienteForEditOutput> GetClienteForEdit(EntityDto<long> input)
         {
            var cliente = await _clienteRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetClienteForEditOutput {Cliente = ObjectMapper.Map<CreateOrEditClienteDto>(cliente)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditClienteDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Clientes_Create)]
		 private async Task Create(CreateOrEditClienteDto input)
         {
            var cliente = ObjectMapper.Map<Cliente>(input);

			

            await _clienteRepository.InsertAsync(cliente);
         }

		 [AbpAuthorize(AppPermissions.Pages_Clientes_Edit)]
		 private async Task Update(CreateOrEditClienteDto input)
         {
            var cliente = await _clienteRepository.FirstOrDefaultAsync((long)input.Id);
             ObjectMapper.Map(input, cliente);
         }

		 [AbpAuthorize(AppPermissions.Pages_Clientes_Delete)]
         public async Task Delete(EntityDto<long> input)
         {
            await _clienteRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetClientesToExcel(GetAllClientesForExcelInput input)
         {
			var generO_CLIENTEFilter = (Generos) input.GENERO_CLIENTEFilter;
			
			var filteredClientes = _clienteRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ID_CLIENTE.Contains(input.Filter) || e.NOMBRE_CLIENTE.Contains(input.Filter) || e.APELLIDO_CLIENTE.Contains(input.Filter) || e.CELULAR_CLIENTE.Contains(input.Filter) || e.DIRECCION_CLIENTE.Contains(input.Filter) || e.DEPARTAMENTO_CLIENTE.Contains(input.Filter) || e.MUNICIPIO_CLIENTE.Contains(input.Filter) || e.EMPRESA_CLIENTE.Contains(input.Filter) || e.PROFESION_CLIENTE.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_CLIENTEFilter),  e => e.ID_CLIENTE.ToLower() == input.ID_CLIENTEFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NOMBRE_CLIENTEFilter),  e => e.NOMBRE_CLIENTE.ToLower() == input.NOMBRE_CLIENTEFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.APELLIDO_CLIENTEFilter),  e => e.APELLIDO_CLIENTE.ToLower() == input.APELLIDO_CLIENTEFilter.ToLower().Trim())
						.WhereIf(input.GENERO_CLIENTEFilter > -1, e => e.GENERO_CLIENTE == generO_CLIENTEFilter)
						.WhereIf(input.MinFECHA_CLIENTEFilter != null, e => e.FECHA_CLIENTE >= input.MinFECHA_CLIENTEFilter)
						.WhereIf(input.MaxFECHA_CLIENTEFilter != null, e => e.FECHA_CLIENTE <= input.MaxFECHA_CLIENTEFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.CELULAR_CLIENTEFilter),  e => e.CELULAR_CLIENTE.ToLower() == input.CELULAR_CLIENTEFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DIRECCION_CLIENTEFilter),  e => e.DIRECCION_CLIENTE.ToLower() == input.DIRECCION_CLIENTEFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DEPARTAMENTO_CLIENTEFilter),  e => e.DEPARTAMENTO_CLIENTE.ToLower() == input.DEPARTAMENTO_CLIENTEFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.MUNICIPIO_CLIENTEFilter),  e => e.MUNICIPIO_CLIENTE.ToLower() == input.MUNICIPIO_CLIENTEFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.EMPRESA_CLIENTEFilter),  e => e.EMPRESA_CLIENTE.ToLower() == input.EMPRESA_CLIENTEFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.PROFESION_CLIENTEFilter),  e => e.PROFESION_CLIENTE.ToLower() == input.PROFESION_CLIENTEFilter.ToLower().Trim());


			var query = (from o in filteredClientes
                         select new GetClienteForViewDto() { 
							Cliente = ObjectMapper.Map<ClienteDto>(o)
						 });


            var clienteListDtos = await query.ToListAsync();

            return _clientesExcelExporter.ExportToFile(clienteListDtos);
         }


    }
}