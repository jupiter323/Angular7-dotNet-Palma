
using System;
using Abp.Application.Services.Dto;

namespace PALMASoft.Municipios.Dtos
{
    public class MunicipioDto : EntityDto<long>
    {
		public string ID_MUNICIPIO { get; set; }

		public string NOMBRE_MUNICIPIO { get; set; }


		 public long DepartamentoId { get; set; }

		 
    }
}