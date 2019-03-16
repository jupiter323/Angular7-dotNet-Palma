using PALMASoft.Fincas;

using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using PALMASoft.Analises.Exporting;
using PALMASoft.Analises.Dtos;
using PALMASoft.Dto;
using Abp.Application.Services.Dto;
using PALMASoft.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace PALMASoft.Analises
{
	[AbpAuthorize(AppPermissions.Pages_Analises)]
    public class AnalisesAppService : PALMASoftAppServiceBase, IAnalisesAppService
    {
		 private readonly IRepository<Analisis, long> _analisisRepository;
		 private readonly IAnalisesExcelExporter _analisesExcelExporter;
		 private readonly IRepository<Finca,long> _fincaRepository;
		 

		  public AnalisesAppService(IRepository<Analisis, long> analisisRepository, IAnalisesExcelExporter analisesExcelExporter , IRepository<Finca, long> fincaRepository) 
		  {
			_analisisRepository = analisisRepository;
			_analisesExcelExporter = analisesExcelExporter;
			_fincaRepository = fincaRepository;
		
		  }

		 public async Task<PagedResultDto<GetAnalisisForViewDto>> GetAll(GetAllAnalisesInput input)
         {
			var tipO_INFORMEFilter = (AnalisisTipo) input.TIPO_INFORMEFilter;
			
			var filteredAnalises = _analisisRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ID_INFORME.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_INFORMEFilter),  e => e.ID_INFORME.ToLower() == input.ID_INFORMEFilter.ToLower().Trim())
						.WhereIf(input.TIPO_INFORMEFilter > -1, e => e.TIPO_INFORME == tipO_INFORMEFilter)
						.WhereIf(input.MinFECHA_MUESTREOFilter != null, e => e.FECHA_MUESTREO >= input.MinFECHA_MUESTREOFilter)
						.WhereIf(input.MaxFECHA_MUESTREOFilter != null, e => e.FECHA_MUESTREO <= input.MaxFECHA_MUESTREOFilter)
						.WhereIf(input.MinFECHA_REGISTROFilter != null, e => e.FECHA_REGISTRO >= input.MinFECHA_REGISTROFilter)
						.WhereIf(input.MaxFECHA_REGISTROFilter != null, e => e.FECHA_REGISTRO <= input.MaxFECHA_REGISTROFilter)
						.WhereIf(input.MinFECHA_ENTREGAFilter != null, e => e.FECHA_ENTREGA >= input.MinFECHA_ENTREGAFilter)
						.WhereIf(input.MaxFECHA_ENTREGAFilter != null, e => e.FECHA_ENTREGA <= input.MaxFECHA_ENTREGAFilter);


			var query = (from o in filteredAnalises
                         join o1 in _fincaRepository.GetAll() on o.FincaId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetAnalisisForViewDto() {
							Analisis = ObjectMapper.Map<AnalisisDto>(o),
                         	FincaNOMBRE_FINCA = s1 == null ? "" : s1.NOMBRE_FINCA.ToString()
						})
						.WhereIf(!string.IsNullOrWhiteSpace(input.FincaNOMBRE_FINCAFilter), e => e.FincaNOMBRE_FINCA.ToLower() == input.FincaNOMBRE_FINCAFilter.ToLower().Trim());

            var totalCount = await query.CountAsync();

            var analises = await query
                .OrderBy(input.Sorting ?? "analisis.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetAnalisisForViewDto>(
                totalCount,
                analises
            );
         }
		 
		 public async Task<GetAnalisisForViewDto> GetAnalisisForView(long id)
         {
            var analisis = await _analisisRepository.GetAsync(id);

            var output = new GetAnalisisForViewDto { Analisis = ObjectMapper.Map<AnalisisDto>(analisis) };

		    if (output.Analisis.FincaId != null)
            {
                var finca = await _fincaRepository.FirstOrDefaultAsync((long)output.Analisis.FincaId);
                output.FincaNOMBRE_FINCA = finca.NOMBRE_FINCA.ToString();
            }
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Analises_Edit)]
		 public async Task<GetAnalisisForEditOutput> GetAnalisisForEdit(EntityDto<long> input)
         {
            var analisis = await _analisisRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetAnalisisForEditOutput {Analisis = ObjectMapper.Map<CreateOrEditAnalisisDto>(analisis)};

		    if (output.Analisis.FincaId != null)
            {
                var finca = await _fincaRepository.FirstOrDefaultAsync((long)output.Analisis.FincaId);
                output.FincaNOMBRE_FINCA = finca.NOMBRE_FINCA.ToString();
            }
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditAnalisisDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Analises_Create)]
		 private async Task Create(CreateOrEditAnalisisDto input)
         {
            var analisis = ObjectMapper.Map<Analisis>(input);

			

            await _analisisRepository.InsertAsync(analisis);
         }

		 [AbpAuthorize(AppPermissions.Pages_Analises_Edit)]
		 private async Task Update(CreateOrEditAnalisisDto input)
         {
            var analisis = await _analisisRepository.FirstOrDefaultAsync((long)input.Id);
             ObjectMapper.Map(input, analisis);
         }

		 [AbpAuthorize(AppPermissions.Pages_Analises_Delete)]
         public async Task Delete(EntityDto<long> input)
         {
            await _analisisRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetAnalisesToExcel(GetAllAnalisesForExcelInput input)
         {
			var tipO_INFORMEFilter = (AnalisisTipo) input.TIPO_INFORMEFilter;
			
			var filteredAnalises = _analisisRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ID_INFORME.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_INFORMEFilter),  e => e.ID_INFORME.ToLower() == input.ID_INFORMEFilter.ToLower().Trim())
						.WhereIf(input.TIPO_INFORMEFilter > -1, e => e.TIPO_INFORME == tipO_INFORMEFilter)
						.WhereIf(input.MinFECHA_MUESTREOFilter != null, e => e.FECHA_MUESTREO >= input.MinFECHA_MUESTREOFilter)
						.WhereIf(input.MaxFECHA_MUESTREOFilter != null, e => e.FECHA_MUESTREO <= input.MaxFECHA_MUESTREOFilter)
						.WhereIf(input.MinFECHA_REGISTROFilter != null, e => e.FECHA_REGISTRO >= input.MinFECHA_REGISTROFilter)
						.WhereIf(input.MaxFECHA_REGISTROFilter != null, e => e.FECHA_REGISTRO <= input.MaxFECHA_REGISTROFilter)
						.WhereIf(input.MinFECHA_ENTREGAFilter != null, e => e.FECHA_ENTREGA >= input.MinFECHA_ENTREGAFilter)
						.WhereIf(input.MaxFECHA_ENTREGAFilter != null, e => e.FECHA_ENTREGA <= input.MaxFECHA_ENTREGAFilter);


			var query = (from o in filteredAnalises
                         join o1 in _fincaRepository.GetAll() on o.FincaId equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()
                         
                         select new GetAnalisisForViewDto() { 
							Analisis = ObjectMapper.Map<AnalisisDto>(o),
                         	FincaNOMBRE_FINCA = s1 == null ? "" : s1.NOMBRE_FINCA.ToString()
						 })
						.WhereIf(!string.IsNullOrWhiteSpace(input.FincaNOMBRE_FINCAFilter), e => e.FincaNOMBRE_FINCA.ToLower() == input.FincaNOMBRE_FINCAFilter.ToLower().Trim());


            var analisisListDtos = await query.ToListAsync();

            return _analisesExcelExporter.ExportToFile(analisisListDtos);
         }



		[AbpAuthorize(AppPermissions.Pages_Analises)]
         public async Task<PagedResultDto<FincaLookupTableDto>> GetAllFincaForLookupTable(GetAllForLookupTableInput input)
         {
             var query = _fincaRepository.GetAll().WhereIf(
                    !string.IsNullOrWhiteSpace(input.Filter),
                   e=> e.NOMBRE_FINCA.ToString().Contains(input.Filter)
                );

            var totalCount = await query.CountAsync();

            var fincaList = await query
                .PageBy(input)
                .ToListAsync();

			var lookupTableDtoList = new List<FincaLookupTableDto>();
			foreach(var finca in fincaList){
				lookupTableDtoList.Add(new FincaLookupTableDto
				{
					Id = finca.Id,
					DisplayName = finca.NOMBRE_FINCA?.ToString()
				});
			}

            return new PagedResultDto<FincaLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
         }
    }
}