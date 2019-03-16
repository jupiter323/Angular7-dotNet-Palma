using PALMASoft.Analises;

using System;
using Abp.Application.Services.Dto;

namespace PALMASoft.Analises.Dtos
{
    public class AnalisisDto : EntityDto<long>
    {
		public string ID_INFORME { get; set; }

		public AnalisisTipo TIPO_INFORME { get; set; }

		public DateTime? FECHA_MUESTREO { get; set; }

		public DateTime? FECHA_REGISTRO { get; set; }

		public DateTime? FECHA_ENTREGA { get; set; }


		 public long FincaId { get; set; }

		 
    }
}