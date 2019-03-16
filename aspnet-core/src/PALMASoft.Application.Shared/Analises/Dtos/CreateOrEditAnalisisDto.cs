using PALMASoft.Analises;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PALMASoft.Analises.Dtos
{
    public class CreateOrEditAnalisisDto : EntityDto<long?>
    {

		[Required]
		public string ID_INFORME { get; set; }
		
		
		public AnalisisTipo TIPO_INFORME { get; set; }
		
		
		public DateTime? FECHA_MUESTREO { get; set; }
		
		
		public DateTime? FECHA_REGISTRO { get; set; }
		
		
		public DateTime? FECHA_ENTREGA { get; set; }
		
		
		 public long FincaId { get; set; }
		 
		 
    }
}