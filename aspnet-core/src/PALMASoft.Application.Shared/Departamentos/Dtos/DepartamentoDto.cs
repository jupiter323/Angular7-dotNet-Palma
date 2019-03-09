
using System;
using Abp.Application.Services.Dto;

namespace PALMASoft.Departamentos.Dtos
{
    public class DepartamentoDto : EntityDto<long>
    {
		public string ID_DEPARTAMENTO { get; set; }

		public string NOMBRE_DEPARTAMENTO { get; set; }


		 public long PaisId { get; set; }

		 
    }
}