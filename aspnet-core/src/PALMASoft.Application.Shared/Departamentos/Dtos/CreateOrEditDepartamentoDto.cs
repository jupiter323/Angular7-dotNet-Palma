
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PALMASoft.Departamentos.Dtos
{
    public class CreateOrEditDepartamentoDto : EntityDto<long?>
    {

		[Required]
		[StringLength(DepartamentoConsts.MaxID_DEPARTAMENTOLength, MinimumLength = DepartamentoConsts.MinID_DEPARTAMENTOLength)]
		public string ID_DEPARTAMENTO { get; set; }
		
		
		[StringLength(DepartamentoConsts.MaxNOMBRE_DEPARTAMENTOLength, MinimumLength = DepartamentoConsts.MinNOMBRE_DEPARTAMENTOLength)]
		public string NOMBRE_DEPARTAMENTO { get; set; }
		
		
		 public long PaisId { get; set; }
		 
		 
    }
}