using Abp.Application.Services.Dto;
using System;

namespace PALMASoft.AFoliares.Dtos
{
    public class GetAllAFoliaresInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string COD_FOLIARFilter { get; set; }

		public string ID_FOLIARFilter { get; set; }

		public string MATERIAL_FOLIARFilter { get; set; }

		public int? MaxNUM_HOJA_FOLIARFilter { get; set; }
		public int? MinNUM_HOJA_FOLIARFilter { get; set; }

		public decimal? MaxNITROGENOFilter { get; set; }
		public decimal? MinNITROGENOFilter { get; set; }

		public decimal? MaxFOSFOROFilter { get; set; }
		public decimal? MinFOSFOROFilter { get; set; }

		public decimal? MaxPOTASIOFilter { get; set; }
		public decimal? MinPOTASIOFilter { get; set; }

		public decimal? MaxCALCIOFilter { get; set; }
		public decimal? MinCALCIOFilter { get; set; }

		public decimal? MaxMAGNESIOFilter { get; set; }
		public decimal? MinMAGNESIOFilter { get; set; }

		public decimal? MaxCLOROFilter { get; set; }
		public decimal? MinCLOROFilter { get; set; }

		public decimal? MaxAZUFREFilter { get; set; }
		public decimal? MinAZUFREFilter { get; set; }

		public decimal? MaxBOROFilter { get; set; }
		public decimal? MinBOROFilter { get; set; }

		public decimal? MaxHIERROFilter { get; set; }
		public decimal? MinHIERROFilter { get; set; }

		public decimal? MaxCOBREFilter { get; set; }
		public decimal? MinCOBREFilter { get; set; }

		public decimal? MaxMANGANESOFilter { get; set; }
		public decimal? MinMANGANESOFilter { get; set; }

		public decimal? MaxZINCFilter { get; set; }
		public decimal? MinZINCFilter { get; set; }

		public decimal? MaxCa_Mg_KFilter { get; set; }
		public decimal? MinCa_Mg_KFilter { get; set; }

		public decimal? MaxCa_Mg_div_KFilter { get; set; }
		public decimal? MinCa_Mg_div_KFilter { get; set; }

		public decimal? MaxN_div_KFilter { get; set; }
		public decimal? MinN_div_KFilter { get; set; }

		public decimal? MaxN_div_PFilter { get; set; }
		public decimal? MinN_div_PFilter { get; set; }

		public decimal? MaxK_div_PFilter { get; set; }
		public decimal? MinK_div_PFilter { get; set; }

		public decimal? MaxCa_div_BFilter { get; set; }
		public decimal? MinCa_div_BFilter { get; set; }

		public string ANALISIS_IDFilter { get; set; }



    }
}