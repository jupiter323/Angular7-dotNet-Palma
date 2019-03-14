using PALMASoft.Clientes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace PALMASoft.Fincas
{
	[Table("Fincas")]
    [Audited]
    public class Finca : FullAuditedEntity<long> 
    {

		[Required]
		[StringLength(FincaConsts.MaxID_FINCALength, MinimumLength = FincaConsts.MinID_FINCALength)]
		public virtual string ID_FINCA { get; set; }
		
		[StringLength(FincaConsts.MaxNOMBRE_FINCALength, MinimumLength = FincaConsts.MinNOMBRE_FINCALength)]
		public virtual string NOMBRE_FINCA { get; set; }
		
		public virtual string DEPARTAMENTO_FINCA { get; set; }
		
		public virtual string MUNICIPIO_FINCA { get; set; }
		
		public virtual string VEREDA_FINCA { get; set; }
		
		public virtual string CORREGIMIENTO_FINCA { get; set; }
		
		public virtual string UBICACION_FINCA { get; set; }
		
		public virtual string LONGITUD_FINCA { get; set; }
		
		public virtual string LATITUD_FINCA { get; set; }
		
		public virtual string CONTACTO_FINCA { get; set; }
		
		public virtual string TELEFONO_FINCA { get; set; }
		
		public virtual string CORREO_FINCA { get; set; }
		

		public virtual long? ClienteId { get; set; }
		public Cliente Cliente { get; set; }
		
    }
}