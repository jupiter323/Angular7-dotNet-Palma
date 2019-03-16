using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace PALMASoft.AFoliares
{
	[Table("AFoliares")]
    [Audited]
    public class AFoliar : FullAuditedEntity<long> 
    {

		[Required]
		[StringLength(AFoliarConsts.MaxCOD_FOLIARLength, MinimumLength = AFoliarConsts.MinCOD_FOLIARLength)]
		public virtual string COD_FOLIAR { get; set; }
		
		[StringLength(AFoliarConsts.MaxID_FOLIARLength, MinimumLength = AFoliarConsts.MinID_FOLIARLength)]
		public virtual string ID_FOLIAR { get; set; }
		
		public virtual string MATERIAL_FOLIAR { get; set; }
		
		public virtual int NUM_HOJA_FOLIAR { get; set; }
		
		public virtual decimal NITROGENO { get; set; }
		
		public virtual decimal FOSFORO { get; set; }
		
		public virtual decimal POTASIO { get; set; }
		
		public virtual decimal CALCIO { get; set; }
		
		public virtual decimal MAGNESIO { get; set; }
		
		public virtual decimal CLORO { get; set; }
		
		public virtual decimal AZUFRE { get; set; }
		
		public virtual decimal BORO { get; set; }
		
		public virtual decimal HIERRO { get; set; }
		
		public virtual decimal COBRE { get; set; }
		
		public virtual decimal MANGANESO { get; set; }
		
		public virtual decimal ZINC { get; set; }
		
		public virtual decimal Ca_Mg_K { get; set; }
		
		public virtual decimal Ca_Mg_div_K { get; set; }
		
		public virtual decimal N_div_K { get; set; }
		
		public virtual decimal N_div_P { get; set; }
		
		public virtual decimal K_div_P { get; set; }
		
		public virtual decimal Ca_div_B { get; set; }
		
		public virtual string ANALISIS_ID { get; set; }
		

    }
}