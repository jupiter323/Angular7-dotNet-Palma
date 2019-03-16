using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace PALMASoft.ASuelos
{
	[Table("ASuelos")]
    [Audited]
    public class ASuelo : FullAuditedEntity<long> 
    {

		public virtual string COD_SUELOS { get; set; }
		
		public virtual string ID_SUELOS { get; set; }
		
		public virtual string MATERIAL_SUELOS { get; set; }
		
		public virtual int PROFUNDIDAD_MUESTRA { get; set; }
		
		public virtual string TEXTURA_SUELOS { get; set; }
		
		public virtual decimal ARENA { get; set; }
		
		public virtual decimal LIMO { get; set; }
		
		public virtual decimal ARCILLA { get; set; }
		
		public virtual decimal PH { get; set; }
		
		public virtual decimal CARBONO_ORGANICO { get; set; }
		
		public virtual decimal MATERIA_ORGANICA { get; set; }
		
		public virtual decimal FOSFORO { get; set; }
		
		public virtual decimal AZUFRE { get; set; }
		
		public virtual decimal? ACIDEZ { get; set; }
		
		public virtual decimal? ALUMINIO { get; set; }
		
		public virtual decimal CALCIO { get; set; }
		
		public virtual decimal MAGNESIO { get; set; }
		
		public virtual decimal POTASIO { get; set; }
		
		public virtual decimal SODIO { get; set; }
		
		public virtual decimal CATIONICO { get; set; }
		
		public virtual decimal ELECTRICA { get; set; }
		
		public virtual decimal BORO { get; set; }
		
		public virtual decimal HIERRO { get; set; }
		
		public virtual decimal COBRE { get; set; }
		
		public virtual decimal MANGANESO { get; set; }
		
		public virtual decimal ZINC { get; set; }
		
		public virtual decimal CICE { get; set; }
		
		public virtual decimal SUMA_BASES { get; set; }
		
		public virtual decimal SAT_BASES { get; set; }
		
		public virtual decimal SAT_K { get; set; }
		
		public virtual decimal SAT_CA { get; set; }
		
		public virtual decimal SAT_MG { get; set; }
		
		public virtual decimal SAT_NA { get; set; }
		
		public virtual decimal SAT_AL { get; set; }
		
		public virtual decimal CA_MG { get; set; }
		
		public virtual decimal K_MG { get; set; }
		
		public virtual decimal CA_MG_DIV_K { get; set; }
		
		public virtual string ANALISIS_ID { get; set; }
		

    }
}