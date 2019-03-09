using PALMASoft.Departamentos;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace PALMASoft.Municipios
{
	[Table("Municipios")]
    [Audited]
    public class Municipio : Entity<long> 
    {

		[Required]
		[StringLength(MunicipioConsts.MaxID_MUNICIPIOLength, MinimumLength = MunicipioConsts.MinID_MUNICIPIOLength)]
		public virtual string ID_MUNICIPIO { get; set; }
		
		[StringLength(MunicipioConsts.MaxNOMBRE_MUNICIPIOLength, MinimumLength = MunicipioConsts.MinNOMBRE_MUNICIPIOLength)]
		public virtual string NOMBRE_MUNICIPIO { get; set; }
		

		public virtual long DepartamentoId { get; set; }
		public Departamento Departamento { get; set; }
		
    }
}