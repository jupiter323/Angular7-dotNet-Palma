
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PALMASoft.Municipios.Dtos
{
    public class CreateOrEditMunicipioDto : EntityDto<long?>
    {

		[Required]
		[StringLength(MunicipioConsts.MaxID_MUNICIPIOLength, MinimumLength = MunicipioConsts.MinID_MUNICIPIOLength)]
		public string ID_MUNICIPIO { get; set; }
		
		
		[StringLength(MunicipioConsts.MaxNOMBRE_MUNICIPIOLength, MinimumLength = MunicipioConsts.MinNOMBRE_MUNICIPIOLength)]
		public string NOMBRE_MUNICIPIO { get; set; }
		
		
		 public long DepartamentoId { get; set; }
		 
		 
    }
}