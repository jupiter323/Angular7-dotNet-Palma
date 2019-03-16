
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using PALMASoft.ASuelos.Exporting;
using PALMASoft.ASuelos.Dtos;
using PALMASoft.Dto;
using Abp.Application.Services.Dto;
using PALMASoft.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace PALMASoft.ASuelos
{
	[AbpAuthorize(AppPermissions.Pages_ASuelos)]
    public class ASuelosAppService : PALMASoftAppServiceBase, IASuelosAppService
    {
		 private readonly IRepository<ASuelo, long> _aSueloRepository;
		 private readonly IASuelosExcelExporter _aSuelosExcelExporter;
		 

		  public ASuelosAppService(IRepository<ASuelo, long> aSueloRepository, IASuelosExcelExporter aSuelosExcelExporter ) 
		  {
			_aSueloRepository = aSueloRepository;
			_aSuelosExcelExporter = aSuelosExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetASueloForViewDto>> GetAll(GetAllASuelosInput input)
         {
			
			var filteredASuelos = _aSueloRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.COD_SUELOS.Contains(input.Filter) || e.ID_SUELOS.Contains(input.Filter) || e.MATERIAL_SUELOS.Contains(input.Filter) || e.TEXTURA_SUELOS.Contains(input.Filter) || e.ANALISIS_ID.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.COD_SUELOSFilter),  e => e.COD_SUELOS.ToLower() == input.COD_SUELOSFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_SUELOSFilter),  e => e.ID_SUELOS.ToLower() == input.ID_SUELOSFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.MATERIAL_SUELOSFilter),  e => e.MATERIAL_SUELOS.ToLower() == input.MATERIAL_SUELOSFilter.ToLower().Trim())
						.WhereIf(input.MinPROFUNDIDAD_MUESTRAFilter != null, e => e.PROFUNDIDAD_MUESTRA >= input.MinPROFUNDIDAD_MUESTRAFilter)
						.WhereIf(input.MaxPROFUNDIDAD_MUESTRAFilter != null, e => e.PROFUNDIDAD_MUESTRA <= input.MaxPROFUNDIDAD_MUESTRAFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TEXTURA_SUELOSFilter),  e => e.TEXTURA_SUELOS.ToLower() == input.TEXTURA_SUELOSFilter.ToLower().Trim())
						.WhereIf(input.MinARENAFilter != null, e => e.ARENA >= input.MinARENAFilter)
						.WhereIf(input.MaxARENAFilter != null, e => e.ARENA <= input.MaxARENAFilter)
						.WhereIf(input.MinLIMOFilter != null, e => e.LIMO >= input.MinLIMOFilter)
						.WhereIf(input.MaxLIMOFilter != null, e => e.LIMO <= input.MaxLIMOFilter)
						.WhereIf(input.MinARCILLAFilter != null, e => e.ARCILLA >= input.MinARCILLAFilter)
						.WhereIf(input.MaxARCILLAFilter != null, e => e.ARCILLA <= input.MaxARCILLAFilter)
						.WhereIf(input.MinPHFilter != null, e => e.PH >= input.MinPHFilter)
						.WhereIf(input.MaxPHFilter != null, e => e.PH <= input.MaxPHFilter)
						.WhereIf(input.MinCARBONO_ORGANICOFilter != null, e => e.CARBONO_ORGANICO >= input.MinCARBONO_ORGANICOFilter)
						.WhereIf(input.MaxCARBONO_ORGANICOFilter != null, e => e.CARBONO_ORGANICO <= input.MaxCARBONO_ORGANICOFilter)
						.WhereIf(input.MinMATERIA_ORGANICAFilter != null, e => e.MATERIA_ORGANICA >= input.MinMATERIA_ORGANICAFilter)
						.WhereIf(input.MaxMATERIA_ORGANICAFilter != null, e => e.MATERIA_ORGANICA <= input.MaxMATERIA_ORGANICAFilter)
						.WhereIf(input.MinFOSFOROFilter != null, e => e.FOSFORO >= input.MinFOSFOROFilter)
						.WhereIf(input.MaxFOSFOROFilter != null, e => e.FOSFORO <= input.MaxFOSFOROFilter)
						.WhereIf(input.MinAZUFREFilter != null, e => e.AZUFRE >= input.MinAZUFREFilter)
						.WhereIf(input.MaxAZUFREFilter != null, e => e.AZUFRE <= input.MaxAZUFREFilter)
						.WhereIf(input.MinACIDEZFilter != null, e => e.ACIDEZ >= input.MinACIDEZFilter)
						.WhereIf(input.MaxACIDEZFilter != null, e => e.ACIDEZ <= input.MaxACIDEZFilter)
						.WhereIf(input.MinALUMINIOFilter != null, e => e.ALUMINIO >= input.MinALUMINIOFilter)
						.WhereIf(input.MaxALUMINIOFilter != null, e => e.ALUMINIO <= input.MaxALUMINIOFilter)
						.WhereIf(input.MinCALCIOFilter != null, e => e.CALCIO >= input.MinCALCIOFilter)
						.WhereIf(input.MaxCALCIOFilter != null, e => e.CALCIO <= input.MaxCALCIOFilter)
						.WhereIf(input.MinMAGNESIOFilter != null, e => e.MAGNESIO >= input.MinMAGNESIOFilter)
						.WhereIf(input.MaxMAGNESIOFilter != null, e => e.MAGNESIO <= input.MaxMAGNESIOFilter)
						.WhereIf(input.MinPOTASIOFilter != null, e => e.POTASIO >= input.MinPOTASIOFilter)
						.WhereIf(input.MaxPOTASIOFilter != null, e => e.POTASIO <= input.MaxPOTASIOFilter)
						.WhereIf(input.MinSODIOFilter != null, e => e.SODIO >= input.MinSODIOFilter)
						.WhereIf(input.MaxSODIOFilter != null, e => e.SODIO <= input.MaxSODIOFilter)
						.WhereIf(input.MinCATIONICOFilter != null, e => e.CATIONICO >= input.MinCATIONICOFilter)
						.WhereIf(input.MaxCATIONICOFilter != null, e => e.CATIONICO <= input.MaxCATIONICOFilter)
						.WhereIf(input.MinELECTRICAFilter != null, e => e.ELECTRICA >= input.MinELECTRICAFilter)
						.WhereIf(input.MaxELECTRICAFilter != null, e => e.ELECTRICA <= input.MaxELECTRICAFilter)
						.WhereIf(input.MinBOROFilter != null, e => e.BORO >= input.MinBOROFilter)
						.WhereIf(input.MaxBOROFilter != null, e => e.BORO <= input.MaxBOROFilter)
						.WhereIf(input.MinHIERROFilter != null, e => e.HIERRO >= input.MinHIERROFilter)
						.WhereIf(input.MaxHIERROFilter != null, e => e.HIERRO <= input.MaxHIERROFilter)
						.WhereIf(input.MinCOBREFilter != null, e => e.COBRE >= input.MinCOBREFilter)
						.WhereIf(input.MaxCOBREFilter != null, e => e.COBRE <= input.MaxCOBREFilter)
						.WhereIf(input.MinMANGANESOFilter != null, e => e.MANGANESO >= input.MinMANGANESOFilter)
						.WhereIf(input.MaxMANGANESOFilter != null, e => e.MANGANESO <= input.MaxMANGANESOFilter)
						.WhereIf(input.MinZINCFilter != null, e => e.ZINC >= input.MinZINCFilter)
						.WhereIf(input.MaxZINCFilter != null, e => e.ZINC <= input.MaxZINCFilter)
						.WhereIf(input.MinCICEFilter != null, e => e.CICE >= input.MinCICEFilter)
						.WhereIf(input.MaxCICEFilter != null, e => e.CICE <= input.MaxCICEFilter)
						.WhereIf(input.MinSUMA_BASESFilter != null, e => e.SUMA_BASES >= input.MinSUMA_BASESFilter)
						.WhereIf(input.MaxSUMA_BASESFilter != null, e => e.SUMA_BASES <= input.MaxSUMA_BASESFilter)
						.WhereIf(input.MinSAT_BASESFilter != null, e => e.SAT_BASES >= input.MinSAT_BASESFilter)
						.WhereIf(input.MaxSAT_BASESFilter != null, e => e.SAT_BASES <= input.MaxSAT_BASESFilter)
						.WhereIf(input.MinSAT_KFilter != null, e => e.SAT_K >= input.MinSAT_KFilter)
						.WhereIf(input.MaxSAT_KFilter != null, e => e.SAT_K <= input.MaxSAT_KFilter)
						.WhereIf(input.MinSAT_CAFilter != null, e => e.SAT_CA >= input.MinSAT_CAFilter)
						.WhereIf(input.MaxSAT_CAFilter != null, e => e.SAT_CA <= input.MaxSAT_CAFilter)
						.WhereIf(input.MinSAT_MGFilter != null, e => e.SAT_MG >= input.MinSAT_MGFilter)
						.WhereIf(input.MaxSAT_MGFilter != null, e => e.SAT_MG <= input.MaxSAT_MGFilter)
						.WhereIf(input.MinSAT_NAFilter != null, e => e.SAT_NA >= input.MinSAT_NAFilter)
						.WhereIf(input.MaxSAT_NAFilter != null, e => e.SAT_NA <= input.MaxSAT_NAFilter)
						.WhereIf(input.MinSAT_ALFilter != null, e => e.SAT_AL >= input.MinSAT_ALFilter)
						.WhereIf(input.MaxSAT_ALFilter != null, e => e.SAT_AL <= input.MaxSAT_ALFilter)
						.WhereIf(input.MinCA_MGFilter != null, e => e.CA_MG >= input.MinCA_MGFilter)
						.WhereIf(input.MaxCA_MGFilter != null, e => e.CA_MG <= input.MaxCA_MGFilter)
						.WhereIf(input.MinK_MGFilter != null, e => e.K_MG >= input.MinK_MGFilter)
						.WhereIf(input.MaxK_MGFilter != null, e => e.K_MG <= input.MaxK_MGFilter)
						.WhereIf(input.MinCA_MG_DIV_KFilter != null, e => e.CA_MG_DIV_K >= input.MinCA_MG_DIV_KFilter)
						.WhereIf(input.MaxCA_MG_DIV_KFilter != null, e => e.CA_MG_DIV_K <= input.MaxCA_MG_DIV_KFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ANALISIS_IDFilter),  e => e.ANALISIS_ID.ToLower() == input.ANALISIS_IDFilter.ToLower().Trim());


			var query = (from o in filteredASuelos
                         select new GetASueloForViewDto() {
							ASuelo = ObjectMapper.Map<ASueloDto>(o)
						});

            var totalCount = await query.CountAsync();

            var aSuelos = await query
                .OrderBy(input.Sorting ?? "aSuelo.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetASueloForViewDto>(
                totalCount,
                aSuelos
            );
         }
		 
		 public async Task<GetASueloForViewDto> GetASueloForView(long id)
         {
            var aSuelo = await _aSueloRepository.GetAsync(id);

            var output = new GetASueloForViewDto { ASuelo = ObjectMapper.Map<ASueloDto>(aSuelo) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_ASuelos_Edit)]
		 public async Task<GetASueloForEditOutput> GetASueloForEdit(EntityDto<long> input)
         {
            var aSuelo = await _aSueloRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetASueloForEditOutput {ASuelo = ObjectMapper.Map<CreateOrEditASueloDto>(aSuelo)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditASueloDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_ASuelos_Create)]
		 private async Task Create(CreateOrEditASueloDto input)
         {
            var aSuelo = ObjectMapper.Map<ASuelo>(input);

			

            await _aSueloRepository.InsertAsync(aSuelo);
         }

		 [AbpAuthorize(AppPermissions.Pages_ASuelos_Edit)]
		 private async Task Update(CreateOrEditASueloDto input)
         {
            var aSuelo = await _aSueloRepository.FirstOrDefaultAsync((long)input.Id);
             ObjectMapper.Map(input, aSuelo);
         }

		 [AbpAuthorize(AppPermissions.Pages_ASuelos_Delete)]
         public async Task Delete(EntityDto<long> input)
         {
            await _aSueloRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetASuelosToExcel(GetAllASuelosForExcelInput input)
         {
			
			var filteredASuelos = _aSueloRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.COD_SUELOS.Contains(input.Filter) || e.ID_SUELOS.Contains(input.Filter) || e.MATERIAL_SUELOS.Contains(input.Filter) || e.TEXTURA_SUELOS.Contains(input.Filter) || e.ANALISIS_ID.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.COD_SUELOSFilter),  e => e.COD_SUELOS.ToLower() == input.COD_SUELOSFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_SUELOSFilter),  e => e.ID_SUELOS.ToLower() == input.ID_SUELOSFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.MATERIAL_SUELOSFilter),  e => e.MATERIAL_SUELOS.ToLower() == input.MATERIAL_SUELOSFilter.ToLower().Trim())
						.WhereIf(input.MinPROFUNDIDAD_MUESTRAFilter != null, e => e.PROFUNDIDAD_MUESTRA >= input.MinPROFUNDIDAD_MUESTRAFilter)
						.WhereIf(input.MaxPROFUNDIDAD_MUESTRAFilter != null, e => e.PROFUNDIDAD_MUESTRA <= input.MaxPROFUNDIDAD_MUESTRAFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.TEXTURA_SUELOSFilter),  e => e.TEXTURA_SUELOS.ToLower() == input.TEXTURA_SUELOSFilter.ToLower().Trim())
						.WhereIf(input.MinARENAFilter != null, e => e.ARENA >= input.MinARENAFilter)
						.WhereIf(input.MaxARENAFilter != null, e => e.ARENA <= input.MaxARENAFilter)
						.WhereIf(input.MinLIMOFilter != null, e => e.LIMO >= input.MinLIMOFilter)
						.WhereIf(input.MaxLIMOFilter != null, e => e.LIMO <= input.MaxLIMOFilter)
						.WhereIf(input.MinARCILLAFilter != null, e => e.ARCILLA >= input.MinARCILLAFilter)
						.WhereIf(input.MaxARCILLAFilter != null, e => e.ARCILLA <= input.MaxARCILLAFilter)
						.WhereIf(input.MinPHFilter != null, e => e.PH >= input.MinPHFilter)
						.WhereIf(input.MaxPHFilter != null, e => e.PH <= input.MaxPHFilter)
						.WhereIf(input.MinCARBONO_ORGANICOFilter != null, e => e.CARBONO_ORGANICO >= input.MinCARBONO_ORGANICOFilter)
						.WhereIf(input.MaxCARBONO_ORGANICOFilter != null, e => e.CARBONO_ORGANICO <= input.MaxCARBONO_ORGANICOFilter)
						.WhereIf(input.MinMATERIA_ORGANICAFilter != null, e => e.MATERIA_ORGANICA >= input.MinMATERIA_ORGANICAFilter)
						.WhereIf(input.MaxMATERIA_ORGANICAFilter != null, e => e.MATERIA_ORGANICA <= input.MaxMATERIA_ORGANICAFilter)
						.WhereIf(input.MinFOSFOROFilter != null, e => e.FOSFORO >= input.MinFOSFOROFilter)
						.WhereIf(input.MaxFOSFOROFilter != null, e => e.FOSFORO <= input.MaxFOSFOROFilter)
						.WhereIf(input.MinAZUFREFilter != null, e => e.AZUFRE >= input.MinAZUFREFilter)
						.WhereIf(input.MaxAZUFREFilter != null, e => e.AZUFRE <= input.MaxAZUFREFilter)
						.WhereIf(input.MinACIDEZFilter != null, e => e.ACIDEZ >= input.MinACIDEZFilter)
						.WhereIf(input.MaxACIDEZFilter != null, e => e.ACIDEZ <= input.MaxACIDEZFilter)
						.WhereIf(input.MinALUMINIOFilter != null, e => e.ALUMINIO >= input.MinALUMINIOFilter)
						.WhereIf(input.MaxALUMINIOFilter != null, e => e.ALUMINIO <= input.MaxALUMINIOFilter)
						.WhereIf(input.MinCALCIOFilter != null, e => e.CALCIO >= input.MinCALCIOFilter)
						.WhereIf(input.MaxCALCIOFilter != null, e => e.CALCIO <= input.MaxCALCIOFilter)
						.WhereIf(input.MinMAGNESIOFilter != null, e => e.MAGNESIO >= input.MinMAGNESIOFilter)
						.WhereIf(input.MaxMAGNESIOFilter != null, e => e.MAGNESIO <= input.MaxMAGNESIOFilter)
						.WhereIf(input.MinPOTASIOFilter != null, e => e.POTASIO >= input.MinPOTASIOFilter)
						.WhereIf(input.MaxPOTASIOFilter != null, e => e.POTASIO <= input.MaxPOTASIOFilter)
						.WhereIf(input.MinSODIOFilter != null, e => e.SODIO >= input.MinSODIOFilter)
						.WhereIf(input.MaxSODIOFilter != null, e => e.SODIO <= input.MaxSODIOFilter)
						.WhereIf(input.MinCATIONICOFilter != null, e => e.CATIONICO >= input.MinCATIONICOFilter)
						.WhereIf(input.MaxCATIONICOFilter != null, e => e.CATIONICO <= input.MaxCATIONICOFilter)
						.WhereIf(input.MinELECTRICAFilter != null, e => e.ELECTRICA >= input.MinELECTRICAFilter)
						.WhereIf(input.MaxELECTRICAFilter != null, e => e.ELECTRICA <= input.MaxELECTRICAFilter)
						.WhereIf(input.MinBOROFilter != null, e => e.BORO >= input.MinBOROFilter)
						.WhereIf(input.MaxBOROFilter != null, e => e.BORO <= input.MaxBOROFilter)
						.WhereIf(input.MinHIERROFilter != null, e => e.HIERRO >= input.MinHIERROFilter)
						.WhereIf(input.MaxHIERROFilter != null, e => e.HIERRO <= input.MaxHIERROFilter)
						.WhereIf(input.MinCOBREFilter != null, e => e.COBRE >= input.MinCOBREFilter)
						.WhereIf(input.MaxCOBREFilter != null, e => e.COBRE <= input.MaxCOBREFilter)
						.WhereIf(input.MinMANGANESOFilter != null, e => e.MANGANESO >= input.MinMANGANESOFilter)
						.WhereIf(input.MaxMANGANESOFilter != null, e => e.MANGANESO <= input.MaxMANGANESOFilter)
						.WhereIf(input.MinZINCFilter != null, e => e.ZINC >= input.MinZINCFilter)
						.WhereIf(input.MaxZINCFilter != null, e => e.ZINC <= input.MaxZINCFilter)
						.WhereIf(input.MinCICEFilter != null, e => e.CICE >= input.MinCICEFilter)
						.WhereIf(input.MaxCICEFilter != null, e => e.CICE <= input.MaxCICEFilter)
						.WhereIf(input.MinSUMA_BASESFilter != null, e => e.SUMA_BASES >= input.MinSUMA_BASESFilter)
						.WhereIf(input.MaxSUMA_BASESFilter != null, e => e.SUMA_BASES <= input.MaxSUMA_BASESFilter)
						.WhereIf(input.MinSAT_BASESFilter != null, e => e.SAT_BASES >= input.MinSAT_BASESFilter)
						.WhereIf(input.MaxSAT_BASESFilter != null, e => e.SAT_BASES <= input.MaxSAT_BASESFilter)
						.WhereIf(input.MinSAT_KFilter != null, e => e.SAT_K >= input.MinSAT_KFilter)
						.WhereIf(input.MaxSAT_KFilter != null, e => e.SAT_K <= input.MaxSAT_KFilter)
						.WhereIf(input.MinSAT_CAFilter != null, e => e.SAT_CA >= input.MinSAT_CAFilter)
						.WhereIf(input.MaxSAT_CAFilter != null, e => e.SAT_CA <= input.MaxSAT_CAFilter)
						.WhereIf(input.MinSAT_MGFilter != null, e => e.SAT_MG >= input.MinSAT_MGFilter)
						.WhereIf(input.MaxSAT_MGFilter != null, e => e.SAT_MG <= input.MaxSAT_MGFilter)
						.WhereIf(input.MinSAT_NAFilter != null, e => e.SAT_NA >= input.MinSAT_NAFilter)
						.WhereIf(input.MaxSAT_NAFilter != null, e => e.SAT_NA <= input.MaxSAT_NAFilter)
						.WhereIf(input.MinSAT_ALFilter != null, e => e.SAT_AL >= input.MinSAT_ALFilter)
						.WhereIf(input.MaxSAT_ALFilter != null, e => e.SAT_AL <= input.MaxSAT_ALFilter)
						.WhereIf(input.MinCA_MGFilter != null, e => e.CA_MG >= input.MinCA_MGFilter)
						.WhereIf(input.MaxCA_MGFilter != null, e => e.CA_MG <= input.MaxCA_MGFilter)
						.WhereIf(input.MinK_MGFilter != null, e => e.K_MG >= input.MinK_MGFilter)
						.WhereIf(input.MaxK_MGFilter != null, e => e.K_MG <= input.MaxK_MGFilter)
						.WhereIf(input.MinCA_MG_DIV_KFilter != null, e => e.CA_MG_DIV_K >= input.MinCA_MG_DIV_KFilter)
						.WhereIf(input.MaxCA_MG_DIV_KFilter != null, e => e.CA_MG_DIV_K <= input.MaxCA_MG_DIV_KFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ANALISIS_IDFilter),  e => e.ANALISIS_ID.ToLower() == input.ANALISIS_IDFilter.ToLower().Trim());


			var query = (from o in filteredASuelos
                         select new GetASueloForViewDto() { 
							ASuelo = ObjectMapper.Map<ASueloDto>(o)
						 });


            var aSueloListDtos = await query.ToListAsync();

            return _aSuelosExcelExporter.ExportToFile(aSueloListDtos);
         }


    }
}