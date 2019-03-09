using PALMASoft.Departamentos;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using PALMASoft.Municipios.Exporting;
using PALMASoft.Municipios.Dtos;
using PALMASoft.Dto;
using Abp.Application.Services.Dto;
using PALMASoft.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace PALMASoft.Municipios
{
	[AbpAuthorize(AppPermissions.Pages_Municipios)]
    public class MunicipiosAppService : PALMASoftAppServiceBase, IMunicipiosAppService
    {
		 private readonly IRepository<Municipio, long> _municipioRepository;
		 private readonly IMunicipiosExcelExporter _municipiosExcelExporter;
		 private readonly IRepository<Departamento,long> _departamentoRepository;
		 

		  public MunicipiosAppService(IRepository<Municipio, long> municipioRepository, IMunicipiosExcelExporter municipiosExcelExporter , IRepository<Departamento, long> departamentoRepository) 
		  {
			_municipioRepository = municipioRepository;
			_municipiosExcelExporter = municipiosExcelExporter;
			_departamentoRepository = departamentoRepository;
		
		  }

		 public async Task<PagedResultDto<GetMunicipioForViewDto>> GetAll(GetAllMunicipiosInput input)
         {
			
			var filteredMunicipios = _municipioRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ID_MUNICIPIO.Contains(input.Filter) || e.NOMBRE_MUNICIPIO.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_MUNICIPIOFilter),  e => e.ID_MUNICIPIO.ToLower() == input.ID_MUNICIPIOFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NOMBRE_MUNICIPIOFilter),  e => e.NOMBRE_MUNICIPIO.ToLower() == input.NOMBRE_MUNICIPIOFilter.ToLower().Trim());


			var query = (from o in filteredMunicipios
                         join o1 in _departamentoRepository.GetAll() on o.DepartamentoId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetMunicipioForViewDto() {
							Municipio = ObjectMapper.Map<MunicipioDto>(o),
                         	DepartamentoNOMBRE_DEPARTAMENTO = s1 == null ? "" : s1.NOMBRE_DEPARTAMENTO.ToString()
						})
						.WhereIf(!string.IsNullOrWhiteSpace(input.DepartamentoNOMBRE_DEPARTAMENTOFilter), e => e.DepartamentoNOMBRE_DEPARTAMENTO.ToLower() == input.DepartamentoNOMBRE_DEPARTAMENTOFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var municipios = await query
                .OrderBy(input.Sorting ?? "municipio.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetMunicipioForViewDto>(
                totalCount,
                municipios
            );
         }
		 
		 public async Task<GetMunicipioForViewDto> GetMunicipioForView(long id)
         {
            var municipio = await _municipioRepository.GetAsync(id);

            var output = new GetMunicipioForViewDto { Municipio = ObjectMapper.Map<MunicipioDto>(municipio) };

		    if (output.Municipio.DepartamentoId != null)
            {
                var departamento = await _departamentoRepository.FirstOrDefaultAsync((long)output.Municipio.DepartamentoId);
                output.DepartamentoNOMBRE_DEPARTAMENTO = departamento.NOMBRE_DEPARTAMENTO.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Municipios_Edit)]
		 public async Task<GetMunicipioForEditOutput> GetMunicipioForEdit(EntityDto<long> input)
         {
            var municipio = await _municipioRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetMunicipioForEditOutput {Municipio = ObjectMapper.Map<CreateOrEditMunicipioDto>(municipio)};

		    if (output.Municipio.DepartamentoId != null)
            {
                var departamento = await _departamentoRepository.FirstOrDefaultAsync((long)output.Municipio.DepartamentoId);
                output.DepartamentoNOMBRE_DEPARTAMENTO = departamento.NOMBRE_DEPARTAMENTO.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditMunicipioDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Municipios_Create)]
		 private async Task Create(CreateOrEditMunicipioDto input)
         {
            var municipio = ObjectMapper.Map<Municipio>(input);

			

            await _municipioRepository.InsertAsync(municipio);
         }

		 [AbpAuthorize(AppPermissions.Pages_Municipios_Edit)]
		 private async Task Update(CreateOrEditMunicipioDto input)
         {
            var municipio = await _municipioRepository.FirstOrDefaultAsync((long)input.Id);
             ObjectMapper.Map(input, municipio);
         }

		 [AbpAuthorize(AppPermissions.Pages_Municipios_Delete)]
         public async Task Delete(EntityDto<long> input)
         {
            await _municipioRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetMunicipiosToExcel(GetAllMunicipiosForExcelInput input)
         {
			
			var filteredMunicipios = _municipioRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ID_MUNICIPIO.Contains(input.Filter) || e.NOMBRE_MUNICIPIO.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_MUNICIPIOFilter),  e => e.ID_MUNICIPIO.ToLower() == input.ID_MUNICIPIOFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NOMBRE_MUNICIPIOFilter),  e => e.NOMBRE_MUNICIPIO.ToLower() == input.NOMBRE_MUNICIPIOFilter.ToLower().Trim());


			var query = (from o in filteredMunicipios
                         join o1 in _departamentoRepository.GetAll() on o.DepartamentoId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetMunicipioForViewDto() { 
							Municipio = ObjectMapper.Map<MunicipioDto>(o),
                         	DepartamentoNOMBRE_DEPARTAMENTO = s1 == null ? "" : s1.NOMBRE_DEPARTAMENTO.ToString()
						 })
						.WhereIf(!string.IsNullOrWhiteSpace(input.DepartamentoNOMBRE_DEPARTAMENTOFilter), e => e.DepartamentoNOMBRE_DEPARTAMENTO.ToLower() == input.DepartamentoNOMBRE_DEPARTAMENTOFilter.ToLower().Trim());


            var municipioListDtos = await query.ToListAsync();

            return _municipiosExcelExporter.ExportToFile(municipioListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_Municipios)]
         public async Task<PagedResultDto<DepartamentoLookupTableDto>> GetAllDepartamentoForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _departamentoRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.NOMBRE_DEPARTAMENTO.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var departamentoList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<DepartamentoLookupTableDto>();
			foreach(var departamento in departamentoList){
				lookupTableDtoList.Add(new DepartamentoLookupTableDto
				{
					Id = departamento.Id,
					DisplayName = departamento.NOMBRE_DEPARTAMENTO?.ToString()
				});
			}

            return new PagedResultDto<DepartamentoLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}