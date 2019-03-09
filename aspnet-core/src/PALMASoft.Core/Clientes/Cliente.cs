using PALMASoft.Clientes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace PALMASoft.Clientes
{
	[Table("Clientes")]
    [Audited]
    public class Cliente : FullAuditedEntity<long> 
    {

		[Required]
		[StringLength(ClienteConsts.MaxID_CLIENTELength, MinimumLength = ClienteConsts.MinID_CLIENTELength)]
		public virtual string ID_CLIENTE { get; set; }
		
		[StringLength(ClienteConsts.MaxNOMBRE_CLIENTELength, MinimumLength = ClienteConsts.MinNOMBRE_CLIENTELength)]
		public virtual string NOMBRE_CLIENTE { get; set; }
		
		[StringLength(ClienteConsts.MaxAPELLIDO_CLIENTELength, MinimumLength = ClienteConsts.MinAPELLIDO_CLIENTELength)]
		public virtual string APELLIDO_CLIENTE { get; set; }
		
		public virtual Generos GENERO_CLIENTE { get; set; }
		
		public virtual DateTime? FECHA_CLIENTE { get; set; }
		
		public virtual string CELULAR_CLIENTE { get; set; }
		
		public virtual string DIRECCION_CLIENTE { get; set; }
		
		public virtual string DEPARTAMENTO_CLIENTE { get; set; }
		
		public virtual string MUNICIPIO_CLIENTE { get; set; }
		
		public virtual string EMPRESA_CLIENTE { get; set; }
		
		public virtual string PROFESION_CLIENTE { get; set; }
		

    }
}