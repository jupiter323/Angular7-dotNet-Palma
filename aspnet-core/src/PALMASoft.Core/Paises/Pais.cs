using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;
using Abp.Auditing;

namespace PALMASoft.Paises
{
	[Table("Paises")]
    [Audited]
    public class Pais : Entity<long> 
    {

		[Required]
		[StringLength(PaisConsts.MaxID_PAISLength, MinimumLength = PaisConsts.MinID_PAISLength)]
		public virtual string ID_PAIS { get; set; }
		
		[StringLength(PaisConsts.MaxNOMBRE_PAISLength, MinimumLength = PaisConsts.MinNOMBRE_PAISLength)]
		public virtual string NOMBRE_PAIS { get; set; }
		

    }
}