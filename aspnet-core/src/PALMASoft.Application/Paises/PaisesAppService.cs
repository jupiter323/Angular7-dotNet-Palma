
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using PALMASoft.Paises.Exporting;
using PALMASoft.Paises.Dtos;
using PALMASoft.Dto;
using Abp.Application.Services.Dto;
using PALMASoft.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;

namespace PALMASoft.Paises
{
	[AbpAuthorize(AppPermissions.Pages_Paises)]
    public class PaisesAppService : PALMASoftAppServiceBase, IPaisesAppService
    {
		 private readonly IRepository<Pais, long> _paisRepository;
		 private readonly IPaisesExcelExporter _paisesExcelExporter;
		 

		  public PaisesAppService(IRepository<Pais, long> paisRepository, IPaisesExcelExporter paisesExcelExporter ) 
		  {
			_paisRepository = paisRepository;
			_paisesExcelExporter = paisesExcelExporter;
			
		  }

		 public async Task<PagedResultDto<GetPaisForViewDto>> GetAll(GetAllPaisesInput input)
         {
			
			var filteredPaises = _paisRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ID_PAIS.Contains(input.Filter) || e.NOMBRE_PAIS.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_PAISFilter),  e => e.ID_PAIS.ToLower() == input.ID_PAISFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NOMBRE_PAISFilter),  e => e.NOMBRE_PAIS.ToLower() == input.NOMBRE_PAISFilter.ToLower().Trim());


			var query = (from o in filteredPaises
                         select new GetPaisForViewDto() {
							Pais = ObjectMapper.Map<PaisDto>(o)
						});

            var totalCount = await query.CountAsync();

            var paises = await query
                .OrderBy(input.Sorting ?? "pais.id asc")
                .PageBy(input)
                .ToListAsync();

            return new PagedResultDto<GetPaisForViewDto>(
                totalCount,
                paises
            );
         }
		 
		 public async Task<GetPaisForViewDto> GetPaisForView(long id)
         {
            var pais = await _paisRepository.GetAsync(id);

            var output = new GetPaisForViewDto { Pais = ObjectMapper.Map<PaisDto>(pais) };
			
            return output;
         }
		 
		 [AbpAuthorize(AppPermissions.Pages_Paises_Edit)]
		 public async Task<GetPaisForEditOutput> GetPaisForEdit(EntityDto<long> input)
         {
            var pais = await _paisRepository.FirstOrDefaultAsync(input.Id);
           
		    var output = new GetPaisForEditOutput {Pais = ObjectMapper.Map<CreateOrEditPaisDto>(pais)};
			
            return output;
         }

		 public async Task CreateOrEdit(CreateOrEditPaisDto input)
         {
            if(input.Id == null){
				await Create(input);
			}
			else{
				await Update(input);
			}
         }

		 [AbpAuthorize(AppPermissions.Pages_Paises_Create)]
		 private async Task Create(CreateOrEditPaisDto input)
         {
            var pais = ObjectMapper.Map<Pais>(input);

			

            await _paisRepository.InsertAsync(pais);
         }

		 [AbpAuthorize(AppPermissions.Pages_Paises_Edit)]
		 private async Task Update(CreateOrEditPaisDto input)
         {
            var pais = await _paisRepository.FirstOrDefaultAsync((long)input.Id);
             ObjectMapper.Map(input, pais);
         }

		 [AbpAuthorize(AppPermissions.Pages_Paises_Delete)]
         public async Task Delete(EntityDto<long> input)
         {
            await _paisRepository.DeleteAsync(input.Id);
         } 

		public async Task<FileDto> GetPaisesToExcel(GetAllPaisesForExcelInput input)
         {
			
			var filteredPaises = _paisRepository.GetAll()
						.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false  || e.ID_PAIS.Contains(input.Filter) || e.NOMBRE_PAIS.Contains(input.Filter))
						.WhereIf(!string.IsNullOrWhiteSpace(input.ID_PAISFilter),  e => e.ID_PAIS.ToLower() == input.ID_PAISFilter.ToLower().Trim())
						.WhereIf(!string.IsNullOrWhiteSpace(input.NOMBRE_PAISFilter),  e => e.NOMBRE_PAIS.ToLower() == input.NOMBRE_PAISFilter.ToLower().Trim());


			var query = (from o in filteredPaises
                         select new GetPaisForViewDto() { 
							Pais = ObjectMapper.Map<PaisDto>(o)
						 });


            var paisListDtos = await query.ToListAsync();

            return _paisesExcelExporter.ExportToFile(paisListDtos);
         }


    }
}