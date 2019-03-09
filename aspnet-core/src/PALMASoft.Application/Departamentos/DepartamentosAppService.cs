using PALMASoft.Paises;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using PALMASoft.Departamentos.Exporting;
using PALMASoft.Departamentos.Dtos;
using PALMASoft.Dto;
using Abp.Application.Services.Dto;
using PALMASoft.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace PALMASoft.Departamentos
{
	[AbpAuthorize(AppPermissions.Pages_Departamentos)]
    public class DepartamentosAppService : PALMASoftAppServiceBase, IDepartamentosAppService
    {
		 private readonly IRepository<Departamento, long> _departamentoRepository;
		 private readonly IDepartamentosExcelExporter _departamentosExcelExporter;
		 private readonly IRepository<Pais,long> _paisRepository;
		 

		  public DepartamentosAppService(IRepository<Departamento, long> departamentoRepository, IDepartamentosExcelExporter departamentosExcelExporter , IRepository<Pais, long> paisRepository) 
		  {
			_departamentoRepository = departamentoRepository;
			_departamentosExcelExporter = departamentosExcelExporter;
			_paisRepository = paisRepository;
		
		  }

		 public async Task<PagedResultDto<GetDepartamentoForViewDto>> GetAll(GetAllDepartamentosInput input)
         {
			
			var filteredDepartamentos = _departamentoRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ID_DEPARTAMENTO.Contains(input.Filter) || e.NOMBRE_DEPARTAMENTO.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_DEPARTAMENTOFilter),  e => e.ID_DEPARTAMENTO.ToLower() == input.ID_DEPARTAMENTOFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NOMBRE_DEPARTAMENTOFilter),  e => e.NOMBRE_DEPARTAMENTO.ToLower() == input.NOMBRE_DEPARTAMENTOFilter.ToLower().Trim());


			var query = (from o in filteredDepartamentos
                         join o1 in _paisRepository.GetAll() on o.PaisId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetDepartamentoForViewDto() {
							Departamento = ObjectMapper.Map<DepartamentoDto>(o),
                         	PaisNOMBRE_PAIS = s1 == null ? "" : s1.NOMBRE_PAIS.ToString()
						})
						.WhereIf(!string.IsNullOrWhiteSpace(input.PaisNOMBRE_PAISFilter), e => e.PaisNOMBRE_PAIS.ToLower() == input.PaisNOMBRE_PAISFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var departamentos = await query
                .OrderBy(input.Sorting ?? "departamento.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetDepartamentoForViewDto>(
                totalCount,
                departamentos
            );
         }
		 
		 public async Task<GetDepartamentoForViewDto> GetDepartamentoForView(long id)
         {
            var departamento = await _departamentoRepository.GetAsync(id);

            var output = new GetDepartamentoForViewDto { Departamento = ObjectMapper.Map<DepartamentoDto>(departamento) };

		    if (output.Departamento.PaisId != null)
            {
                var pais = await _paisRepository.FirstOrDefaultAsync((long)output.Departamento.PaisId);
                output.PaisNOMBRE_PAIS = pais.NOMBRE_PAIS.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Departamentos_Edit)]
		 public async Task<GetDepartamentoForEditOutput> GetDepartamentoForEdit(EntityDto<long> input)
         {
            var departamento = await _departamentoRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetDepartamentoForEditOutput {Departamento = ObjectMapper.Map<CreateOrEditDepartamentoDto>(departamento)};

		    if (output.Departamento.PaisId != null)
            {
                var pais = await _paisRepository.FirstOrDefaultAsync((long)output.Departamento.PaisId);
                output.PaisNOMBRE_PAIS = pais.NOMBRE_PAIS.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditDepartamentoDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Departamentos_Create)]
		 private async Task Create(CreateOrEditDepartamentoDto input)
         {
            var departamento = ObjectMapper.Map<Departamento>(input);

			

            await _departamentoRepository.InsertAsync(departamento);
         }

		 [AbpAuthorize(AppPermissions.Pages_Departamentos_Edit)]
		 private async Task Update(CreateOrEditDepartamentoDto input)
         {
            var departamento = await _departamentoRepository.FirstOrDefaultAsync((long)input.Id);
             ObjectMapper.Map(input, departamento);
         }

		 [AbpAuthorize(AppPermissions.Pages_Departamentos_Delete)]
         public async Task Delete(EntityDto<long> input)
         {
            await _departamentoRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetDepartamentosToExcel(GetAllDepartamentosForExcelInput input)
         {
			
			var filteredDepartamentos = _departamentoRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ID_DEPARTAMENTO.Contains(input.Filter) || e.NOMBRE_DEPARTAMENTO.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_DEPARTAMENTOFilter),  e => e.ID_DEPARTAMENTO.ToLower() == input.ID_DEPARTAMENTOFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NOMBRE_DEPARTAMENTOFilter),  e => e.NOMBRE_DEPARTAMENTO.ToLower() == input.NOMBRE_DEPARTAMENTOFilter.ToLower().Trim());


			var query = (from o in filteredDepartamentos
                         join o1 in _paisRepository.GetAll() on o.PaisId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetDepartamentoForViewDto() { 
							Departamento = ObjectMapper.Map<DepartamentoDto>(o),
                         	PaisNOMBRE_PAIS = s1 == null ? "" : s1.NOMBRE_PAIS.ToString()
						 })
						.WhereIf(!string.IsNullOrWhiteSpace(input.PaisNOMBRE_PAISFilter), e => e.PaisNOMBRE_PAIS.ToLower() == input.PaisNOMBRE_PAISFilter.ToLower().Trim());


            var departamentoListDtos = await query.ToListAsync();

            return _departamentosExcelExporter.ExportToFile(departamentoListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_Departamentos)]
         public async Task<PagedResultDto<PaisLookupTableDto>> GetAllPaisForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _paisRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.NOMBRE_PAIS.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var paisList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<PaisLookupTableDto>();
			foreach(var pais in paisList){
				lookupTableDtoList.Add(new PaisLookupTableDto
				{
					Id = pais.Id,
					DisplayName = pais.NOMBRE_PAIS?.ToString()
				});
			}

            return new PagedResultDto<PaisLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}