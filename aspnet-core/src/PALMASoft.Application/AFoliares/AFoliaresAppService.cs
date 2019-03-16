
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using PALMASoft.AFoliares.Exporting;
using PALMASoft.AFoliares.Dtos;
using PALMASoft.Dto;
using Abp.Application.Services.Dto;
using PALMASoft.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace PALMASoft.AFoliares
{
	[AbpAuthorize(AppPermissions.Pages_AFoliares)]
    public class AFoliaresAppService : PALMASoftAppServiceBase, IAFoliaresAppService
    {
		 private readonly IRepository<AFoliar, long> _aFoliarRepository;
		 private readonly IAFoliaresExcelExporter _aFoliaresExcelExporter;
		 

		  public AFoliaresAppService(IRepository<AFoliar, long> aFoliarRepository, IAFoliaresExcelExporter aFoliaresExcelExporter ) 
		  {
			_aFoliarRepository = aFoliarRepository;
			_aFoliaresExcelExporter = aFoliaresExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetAFoliarForViewDto>> GetAll(GetAllAFoliaresInput input)
         {
			
			var filteredAFoliares = _aFoliarRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.COD_FOLIAR.Contains(input.Filter) || e.ID_FOLIAR.Contains(input.Filter) || e.MATERIAL_FOLIAR.Contains(input.Filter) || e.ANALISIS_ID.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.COD_FOLIARFilter),  e => e.COD_FOLIAR.ToLower() == input.COD_FOLIARFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_FOLIARFilter),  e => e.ID_FOLIAR.ToLower() == input.ID_FOLIARFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.MATERIAL_FOLIARFilter),  e => e.MATERIAL_FOLIAR.ToLower() == input.MATERIAL_FOLIARFilter.ToLower().Trim())
						.WhereIf(input.MinNUM_HOJA_FOLIARFilter != null, e => e.NUM_HOJA_FOLIAR >= input.MinNUM_HOJA_FOLIARFilter)
						.WhereIf(input.MaxNUM_HOJA_FOLIARFilter != null, e => e.NUM_HOJA_FOLIAR <= input.MaxNUM_HOJA_FOLIARFilter)
						.WhereIf(input.MinNITROGENOFilter != null, e => e.NITROGENO >= input.MinNITROGENOFilter)
						.WhereIf(input.MaxNITROGENOFilter != null, e => e.NITROGENO <= input.MaxNITROGENOFilter)
						.WhereIf(input.MinFOSFOROFilter != null, e => e.FOSFORO >= input.MinFOSFOROFilter)
						.WhereIf(input.MaxFOSFOROFilter != null, e => e.FOSFORO <= input.MaxFOSFOROFilter)
						.WhereIf(input.MinPOTASIOFilter != null, e => e.POTASIO >= input.MinPOTASIOFilter)
						.WhereIf(input.MaxPOTASIOFilter != null, e => e.POTASIO <= input.MaxPOTASIOFilter)
						.WhereIf(input.MinCALCIOFilter != null, e => e.CALCIO >= input.MinCALCIOFilter)
						.WhereIf(input.MaxCALCIOFilter != null, e => e.CALCIO <= input.MaxCALCIOFilter)
						.WhereIf(input.MinMAGNESIOFilter != null, e => e.MAGNESIO >= input.MinMAGNESIOFilter)
						.WhereIf(input.MaxMAGNESIOFilter != null, e => e.MAGNESIO <= input.MaxMAGNESIOFilter)
						.WhereIf(input.MinCLOROFilter != null, e => e.CLORO >= input.MinCLOROFilter)
						.WhereIf(input.MaxCLOROFilter != null, e => e.CLORO <= input.MaxCLOROFilter)
						.WhereIf(input.MinAZUFREFilter != null, e => e.AZUFRE >= input.MinAZUFREFilter)
						.WhereIf(input.MaxAZUFREFilter != null, e => e.AZUFRE <= input.MaxAZUFREFilter)
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
						.WhereIf(input.MinCa_Mg_KFilter != null, e => e.Ca_Mg_K >= input.MinCa_Mg_KFilter)
						.WhereIf(input.MaxCa_Mg_KFilter != null, e => e.Ca_Mg_K <= input.MaxCa_Mg_KFilter)
						.WhereIf(input.MinCa_Mg_div_KFilter != null, e => e.Ca_Mg_div_K >= input.MinCa_Mg_div_KFilter)
						.WhereIf(input.MaxCa_Mg_div_KFilter != null, e => e.Ca_Mg_div_K <= input.MaxCa_Mg_div_KFilter)
						.WhereIf(input.MinN_div_KFilter != null, e => e.N_div_K >= input.MinN_div_KFilter)
						.WhereIf(input.MaxN_div_KFilter != null, e => e.N_div_K <= input.MaxN_div_KFilter)
						.WhereIf(input.MinN_div_PFilter != null, e => e.N_div_P >= input.MinN_div_PFilter)
						.WhereIf(input.MaxN_div_PFilter != null, e => e.N_div_P <= input.MaxN_div_PFilter)
						.WhereIf(input.MinK_div_PFilter != null, e => e.K_div_P >= input.MinK_div_PFilter)
						.WhereIf(input.MaxK_div_PFilter != null, e => e.K_div_P <= input.MaxK_div_PFilter)
						.WhereIf(input.MinCa_div_BFilter != null, e => e.Ca_div_B >= input.MinCa_div_BFilter)
						.WhereIf(input.MaxCa_div_BFilter != null, e => e.Ca_div_B <= input.MaxCa_div_BFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ANALISIS_IDFilter),  e => e.ANALISIS_ID.ToLower() == input.ANALISIS_IDFilter.ToLower().Trim());


			var query = (from o in filteredAFoliares
                         select new GetAFoliarForViewDto() {
							AFoliar = ObjectMapper.Map<AFoliarDto>(o)
						});

            var totalCount = await query.CountAsync();

            var aFoliares = await query
                .OrderBy(input.Sorting ?? "aFoliar.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetAFoliarForViewDto>(
                totalCount,
                aFoliares
            );
         }
		 
		 public async Task<GetAFoliarForViewDto> GetAFoliarForView(long id)
         {
            var aFoliar = await _aFoliarRepository.GetAsync(id);

            var output = new GetAFoliarForViewDto { AFoliar = ObjectMapper.Map<AFoliarDto>(aFoliar) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_AFoliares_Edit)]
		 public async Task<GetAFoliarForEditOutput> GetAFoliarForEdit(EntityDto<long> input)
         {
            var aFoliar = await _aFoliarRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetAFoliarForEditOutput {AFoliar = ObjectMapper.Map<CreateOrEditAFoliarDto>(aFoliar)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditAFoliarDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_AFoliares_Create)]
		 private async Task Create(CreateOrEditAFoliarDto input)
         {
            var aFoliar = ObjectMapper.Map<AFoliar>(input);

			

            await _aFoliarRepository.InsertAsync(aFoliar);
         }

		 [AbpAuthorize(AppPermissions.Pages_AFoliares_Edit)]
		 private async Task Update(CreateOrEditAFoliarDto input)
         {
            var aFoliar = await _aFoliarRepository.FirstOrDefaultAsync((long)input.Id);
             ObjectMapper.Map(input, aFoliar);
         }

		 [AbpAuthorize(AppPermissions.Pages_AFoliares_Delete)]
         public async Task Delete(EntityDto<long> input)
         {
            await _aFoliarRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetAFoliaresToExcel(GetAllAFoliaresForExcelInput input)
         {
			
			var filteredAFoliares = _aFoliarRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.COD_FOLIAR.Contains(input.Filter) || e.ID_FOLIAR.Contains(input.Filter) || e.MATERIAL_FOLIAR.Contains(input.Filter) || e.ANALISIS_ID.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.COD_FOLIARFilter),  e => e.COD_FOLIAR.ToLower() == input.COD_FOLIARFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_FOLIARFilter),  e => e.ID_FOLIAR.ToLower() == input.ID_FOLIARFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.MATERIAL_FOLIARFilter),  e => e.MATERIAL_FOLIAR.ToLower() == input.MATERIAL_FOLIARFilter.ToLower().Trim())
						.WhereIf(input.MinNUM_HOJA_FOLIARFilter != null, e => e.NUM_HOJA_FOLIAR >= input.MinNUM_HOJA_FOLIARFilter)
						.WhereIf(input.MaxNUM_HOJA_FOLIARFilter != null, e => e.NUM_HOJA_FOLIAR <= input.MaxNUM_HOJA_FOLIARFilter)
						.WhereIf(input.MinNITROGENOFilter != null, e => e.NITROGENO >= input.MinNITROGENOFilter)
						.WhereIf(input.MaxNITROGENOFilter != null, e => e.NITROGENO <= input.MaxNITROGENOFilter)
						.WhereIf(input.MinFOSFOROFilter != null, e => e.FOSFORO >= input.MinFOSFOROFilter)
						.WhereIf(input.MaxFOSFOROFilter != null, e => e.FOSFORO <= input.MaxFOSFOROFilter)
						.WhereIf(input.MinPOTASIOFilter != null, e => e.POTASIO >= input.MinPOTASIOFilter)
						.WhereIf(input.MaxPOTASIOFilter != null, e => e.POTASIO <= input.MaxPOTASIOFilter)
						.WhereIf(input.MinCALCIOFilter != null, e => e.CALCIO >= input.MinCALCIOFilter)
						.WhereIf(input.MaxCALCIOFilter != null, e => e.CALCIO <= input.MaxCALCIOFilter)
						.WhereIf(input.MinMAGNESIOFilter != null, e => e.MAGNESIO >= input.MinMAGNESIOFilter)
						.WhereIf(input.MaxMAGNESIOFilter != null, e => e.MAGNESIO <= input.MaxMAGNESIOFilter)
						.WhereIf(input.MinCLOROFilter != null, e => e.CLORO >= input.MinCLOROFilter)
						.WhereIf(input.MaxCLOROFilter != null, e => e.CLORO <= input.MaxCLOROFilter)
						.WhereIf(input.MinAZUFREFilter != null, e => e.AZUFRE >= input.MinAZUFREFilter)
						.WhereIf(input.MaxAZUFREFilter != null, e => e.AZUFRE <= input.MaxAZUFREFilter)
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
						.WhereIf(input.MinCa_Mg_KFilter != null, e => e.Ca_Mg_K >= input.MinCa_Mg_KFilter)
						.WhereIf(input.MaxCa_Mg_KFilter != null, e => e.Ca_Mg_K <= input.MaxCa_Mg_KFilter)
						.WhereIf(input.MinCa_Mg_div_KFilter != null, e => e.Ca_Mg_div_K >= input.MinCa_Mg_div_KFilter)
						.WhereIf(input.MaxCa_Mg_div_KFilter != null, e => e.Ca_Mg_div_K <= input.MaxCa_Mg_div_KFilter)
						.WhereIf(input.MinN_div_KFilter != null, e => e.N_div_K >= input.MinN_div_KFilter)
						.WhereIf(input.MaxN_div_KFilter != null, e => e.N_div_K <= input.MaxN_div_KFilter)
						.WhereIf(input.MinN_div_PFilter != null, e => e.N_div_P >= input.MinN_div_PFilter)
						.WhereIf(input.MaxN_div_PFilter != null, e => e.N_div_P <= input.MaxN_div_PFilter)
						.WhereIf(input.MinK_div_PFilter != null, e => e.K_div_P >= input.MinK_div_PFilter)
						.WhereIf(input.MaxK_div_PFilter != null, e => e.K_div_P <= input.MaxK_div_PFilter)
						.WhereIf(input.MinCa_div_BFilter != null, e => e.Ca_div_B >= input.MinCa_div_BFilter)
						.WhereIf(input.MaxCa_div_BFilter != null, e => e.Ca_div_B <= input.MaxCa_div_BFilter)
						.WhereIf(!string.IsNullOrWhiteSpace(input.ANALISIS_IDFilter),  e => e.ANALISIS_ID.ToLower() == input.ANALISIS_IDFilter.ToLower().Trim());


			var query = (from o in filteredAFoliares
                         select new GetAFoliarForViewDto() { 
							AFoliar = ObjectMapper.Map<AFoliarDto>(o)
						 });


            var aFoliarListDtos = await query.ToListAsync();

            return _aFoliaresExcelExporter.ExportToFile(aFoliarListDtos);
         }


    }
}