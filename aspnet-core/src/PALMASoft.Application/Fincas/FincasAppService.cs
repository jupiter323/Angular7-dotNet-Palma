using PALMASoft.Clientes;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using PALMASoft.Fincas.Exporting;
using PALMASoft.Fincas.Dtos;
using PALMASoft.Dto;
using Abp.Application.Services.Dto;
using PALMASoft.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace PALMASoft.Fincas
{
	[AbpAuthorize(AppPermissions.Pages_Fincas)]
    public class FincasAppService : PALMASoftAppServiceBase, IFincasAppService
    {
		 private readonly IRepository<Finca, long> _fincaRepository;
		 private readonly IFincasExcelExporter _fincasExcelExporter;
		 private readonly IRepository<Cliente,long> _clienteRepository;
		 

		  public FincasAppService(IRepository<Finca, long> fincaRepository, IFincasExcelExporter fincasExcelExporter , IRepository<Cliente, long> clienteRepository) 
		  {
			_fincaRepository = fincaRepository;
			_fincasExcelExporter = fincasExcelExporter;
			_clienteRepository = clienteRepository;
		
		  }

		 public async Task<PagedResultDto<GetFincaForViewDto>> GetAll(GetAllFincasInput input)
         {
			
			var filteredFincas = _fincaRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ID_FINCA.Contains(input.Filter) || e.NOMBRE_FINCA.Contains(input.Filter) || e.DEPARTAMENTO_FINCA.Contains(input.Filter) || e.MUNICIPIO_FINCA.Contains(input.Filter) || e.VEREDA_FINCA.Contains(input.Filter) || e.CORREGIMIENTO_FINCA.Contains(input.Filter) || e.UBICACION_FINCA.Contains(input.Filter) || e.LONGITUD_FINCA.Contains(input.Filter) || e.LATITUD_FINCA.Contains(input.Filter) || e.CONTACTO_FINCA.Contains(input.Filter) || e.TELEFONO_FINCA.Contains(input.Filter) || e.CORREO_FINCA.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_FINCAFilter),  e => e.ID_FINCA.ToLower() == input.ID_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NOMBRE_FINCAFilter),  e => e.NOMBRE_FINCA.ToLower() == input.NOMBRE_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DEPARTAMENTO_FINCAFilter),  e => e.DEPARTAMENTO_FINCA.ToLower() == input.DEPARTAMENTO_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.MUNICIPIO_FINCAFilter),  e => e.MUNICIPIO_FINCA.ToLower() == input.MUNICIPIO_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.VEREDA_FINCAFilter),  e => e.VEREDA_FINCA.ToLower() == input.VEREDA_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CORREGIMIENTO_FINCAFilter),  e => e.CORREGIMIENTO_FINCA.ToLower() == input.CORREGIMIENTO_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.UBICACION_FINCAFilter),  e => e.UBICACION_FINCA.ToLower() == input.UBICACION_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.LONGITUD_FINCAFilter),  e => e.LONGITUD_FINCA.ToLower() == input.LONGITUD_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.LATITUD_FINCAFilter),  e => e.LATITUD_FINCA.ToLower() == input.LATITUD_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CONTACTO_FINCAFilter),  e => e.CONTACTO_FINCA.ToLower() == input.CONTACTO_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.TELEFONO_FINCAFilter),  e => e.TELEFONO_FINCA.ToLower() == input.TELEFONO_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CORREO_FINCAFilter),  e => e.CORREO_FINCA.ToLower() == input.CORREO_FINCAFilter.ToLower().Trim());


			var query = (from o in filteredFincas
                         join o1 in _clienteRepository.GetAll() on o.ClienteId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetFincaForViewDto() {
							Finca = ObjectMapper.Map<FincaDto>(o),
                         	ClienteNOMBRE_CLIENTE = s1 == null ? "" : s1.NOMBRE_CLIENTE.ToString()
						})
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClienteNOMBRE_CLIENTEFilter), e => e.ClienteNOMBRE_CLIENTE.ToLower() == input.ClienteNOMBRE_CLIENTEFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var fincas = await query
                .OrderBy(input.Sorting ?? "finca.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetFincaForViewDto>(
                totalCount,
                fincas
            );
         }
		 
		 public async Task<GetFincaForViewDto> GetFincaForView(long id)
         {
            var finca = await _fincaRepository.GetAsync(id);

            var output = new GetFincaForViewDto { Finca = ObjectMapper.Map<FincaDto>(finca) };

		    if (output.Finca.ClienteId != null)
            {
                var cliente = await _clienteRepository.FirstOrDefaultAsync((long)output.Finca.ClienteId);
                output.ClienteNOMBRE_CLIENTE = cliente.NOMBRE_CLIENTE.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Fincas_Edit)]
		 public async Task<GetFincaForEditOutput> GetFincaForEdit(EntityDto<long> input)
         {
            var finca = await _fincaRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetFincaForEditOutput {Finca = ObjectMapper.Map<CreateOrEditFincaDto>(finca)};

		    if (output.Finca.ClienteId != null)
            {
                var cliente = await _clienteRepository.FirstOrDefaultAsync((long)output.Finca.ClienteId);
                output.ClienteNOMBRE_CLIENTE = cliente.NOMBRE_CLIENTE.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditFincaDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Fincas_Create)]
		 private async Task Create(CreateOrEditFincaDto input)
         {
            var finca = ObjectMapper.Map<Finca>(input);

			

            await _fincaRepository.InsertAsync(finca);
         }

		 [AbpAuthorize(AppPermissions.Pages_Fincas_Edit)]
		 private async Task Update(CreateOrEditFincaDto input)
         {
            var finca = await _fincaRepository.FirstOrDefaultAsync((long)input.Id);
             ObjectMapper.Map(input, finca);
         }

		 [AbpAuthorize(AppPermissions.Pages_Fincas_Delete)]
         public async Task Delete(EntityDto<long> input)
         {
            await _fincaRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetFincasToExcel(GetAllFincasForExcelInput input)
         {
			
			var filteredFincas = _fincaRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ID_FINCA.Contains(input.Filter) || e.NOMBRE_FINCA.Contains(input.Filter) || e.DEPARTAMENTO_FINCA.Contains(input.Filter) || e.MUNICIPIO_FINCA.Contains(input.Filter) || e.VEREDA_FINCA.Contains(input.Filter) || e.CORREGIMIENTO_FINCA.Contains(input.Filter) || e.UBICACION_FINCA.Contains(input.Filter) || e.LONGITUD_FINCA.Contains(input.Filter) || e.LATITUD_FINCA.Contains(input.Filter) || e.CONTACTO_FINCA.Contains(input.Filter) || e.TELEFONO_FINCA.Contains(input.Filter) || e.CORREO_FINCA.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_FINCAFilter),  e => e.ID_FINCA.ToLower() == input.ID_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NOMBRE_FINCAFilter),  e => e.NOMBRE_FINCA.ToLower() == input.NOMBRE_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.DEPARTAMENTO_FINCAFilter),  e => e.DEPARTAMENTO_FINCA.ToLower() == input.DEPARTAMENTO_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.MUNICIPIO_FINCAFilter),  e => e.MUNICIPIO_FINCA.ToLower() == input.MUNICIPIO_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.VEREDA_FINCAFilter),  e => e.VEREDA_FINCA.ToLower() == input.VEREDA_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CORREGIMIENTO_FINCAFilter),  e => e.CORREGIMIENTO_FINCA.ToLower() == input.CORREGIMIENTO_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.UBICACION_FINCAFilter),  e => e.UBICACION_FINCA.ToLower() == input.UBICACION_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.LONGITUD_FINCAFilter),  e => e.LONGITUD_FINCA.ToLower() == input.LONGITUD_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.LATITUD_FINCAFilter),  e => e.LATITUD_FINCA.ToLower() == input.LATITUD_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CONTACTO_FINCAFilter),  e => e.CONTACTO_FINCA.ToLower() == input.CONTACTO_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.TELEFONO_FINCAFilter),  e => e.TELEFONO_FINCA.ToLower() == input.TELEFONO_FINCAFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.CORREO_FINCAFilter),  e => e.CORREO_FINCA.ToLower() == input.CORREO_FINCAFilter.ToLower().Trim());


			var query = (from o in filteredFincas
                         join o1 in _clienteRepository.GetAll() on o.ClienteId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetFincaForViewDto() { 
							Finca = ObjectMapper.Map<FincaDto>(o),
                         	ClienteNOMBRE_CLIENTE = s1 == null ? "" : s1.NOMBRE_CLIENTE.ToString()
						 })
						.WhereIf(!string.IsNullOrWhiteSpace(input.ClienteNOMBRE_CLIENTEFilter), e => e.ClienteNOMBRE_CLIENTE.ToLower() == input.ClienteNOMBRE_CLIENTEFilter.ToLower().Trim());


            var fincaListDtos = await query.ToListAsync();

            return _fincasExcelExporter.ExportToFile(fincaListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_Fincas)]
         public async Task<PagedResultDto<ClienteLookupTableDto>> GetAllClienteForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _clienteRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.NOMBRE_CLIENTE.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var clienteList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<ClienteLookupTableDto>();
			foreach(var cliente in clienteList){
				lookupTableDtoList.Add(new ClienteLookupTableDto
				{
					Id = cliente.Id,
					DisplayName = cliente.NOMBRE_CLIENTE?.ToString()
				});
			}

            return new PagedResultDto<ClienteLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}