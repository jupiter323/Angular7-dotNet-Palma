using PALMASoft.Paises;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace PALMASoft.Departamentos
{
	[Table("Departamentos")]
    [Audited]
    public class Departamento : Entity<long> 
    {

		[Required]
		[StringLength(DepartamentoConsts.MaxID_DEPARTAMENTOLength, MinimumLength = DepartamentoConsts.MinID_DEPARTAMENTOLength)]
		public virtual string ID_DEPARTAMENTO { get; set; }
		
		[StringLength(DepartamentoConsts.MaxNOMBRE_DEPARTAMENTOLength, MinimumLength = DepartamentoConsts.MinNOMBRE_DEPARTAMENTOLength)]
		public virtual string NOMBRE_DEPARTAMENTO { get; set; }
		

		public virtual long PaisId { get; set; }
		public Pais Pais { get; set; }
		
    }
}