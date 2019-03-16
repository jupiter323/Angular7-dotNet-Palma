using PALMASoft.Analises;
using PALMASoft.Fincas;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace PALMASoft.Analises
{
	[Table("Analises")]
    public class Analisis : CreationAuditedEntity<long> 
    {

		[Required]
		public virtual string ID_INFORME { get; set; }
		
		public virtual AnalisisTipo TIPO_INFORME { get; set; }
		
		public virtual DateTime? FECHA_MUESTREO { get; set; }
		
		public virtual DateTime? FECHA_REGISTRO { get; set; }
		
		public virtual DateTime? FECHA_ENTREGA { get; set; }
		

		public virtual long FincaId { get; set; }
		public Finca Finca { get; set; }
		
    }
}