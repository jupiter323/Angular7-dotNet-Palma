
using System;
using Abp.Application.Services.Dto;

namespace PALMASoft.Paises.Dtos
{
    public class PaisDto : EntityDto<long>
    {
		public string ID_PAIS { get; set; }

		public string NOMBRE_PAIS { get; set; }



    }
}