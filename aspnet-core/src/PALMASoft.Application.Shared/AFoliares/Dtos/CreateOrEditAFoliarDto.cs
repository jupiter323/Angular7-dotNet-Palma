
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PALMASoft.AFoliares.Dtos
{
    public class CreateOrEditAFoliarDto : EntityDto<long?>
    {

		[Required]
		[StringLength(AFoliarConsts.MaxCOD_FOLIARLength, MinimumLength = AFoliarConsts.MinCOD_FOLIARLength)]
		public string COD_FOLIAR { get; set; }
		
		
		[StringLength(AFoliarConsts.MaxID_FOLIARLength, MinimumLength = AFoliarConsts.MinID_FOLIARLength)]
		public string ID_FOLIAR { get; set; }
		
		
		public string MATERIAL_FOLIAR { get; set; }
		
		
		public int NUM_HOJA_FOLIAR { get; set; }
		
		
		public decimal NITROGENO { get; set; }
		
		
		public decimal FOSFORO { get; set; }
		
		
		public decimal POTASIO { get; set; }
		
		
		public decimal CALCIO { get; set; }
		
		
		public decimal MAGNESIO { get; set; }
		
		
		public decimal CLORO { get; set; }
		
		
		public decimal AZUFRE { get; set; }
		
		
		public decimal BORO { get; set; }
		
		
		public decimal HIERRO { get; set; }
		
		
		public decimal COBRE { get; set; }
		
		
		public decimal MANGANESO { get; set; }
		
		
		public decimal ZINC { get; set; }
		
		
		public decimal Ca_Mg_K { get; set; }
		
		
		public decimal Ca_Mg_div_K { get; set; }
		
		
		public decimal N_div_K { get; set; }
		
		
		public decimal N_div_P { get; set; }
		
		
		public decimal K_div_P { get; set; }
		
		
		public decimal Ca_div_B { get; set; }
		
		
		public string ANALISIS_ID { get; set; }
		
		

    }
}