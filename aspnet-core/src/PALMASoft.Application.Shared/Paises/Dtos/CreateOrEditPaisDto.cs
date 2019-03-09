
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PALMASoft.Paises.Dtos
{
    public class CreateOrEditPaisDto : EntityDto<long?>
    {

		[Required]
		[StringLength(PaisConsts.MaxID_PAISLength, MinimumLength = PaisConsts.MinID_PAISLength)]
		public string ID_PAIS { get; set; }
		
		
		[StringLength(PaisConsts.MaxNOMBRE_PAISLength, MinimumLength = PaisConsts.MinNOMBRE_PAISLength)]
		public string NOMBRE_PAIS { get; set; }
		
		

    }
}