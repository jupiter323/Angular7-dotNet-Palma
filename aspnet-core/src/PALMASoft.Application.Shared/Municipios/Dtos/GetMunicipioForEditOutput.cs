using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PALMASoft.Municipios.Dtos
{
    public class GetMunicipioForEditOutput
    {
		public CreateOrEditMunicipioDto Municipio { get; set; }

		public string DepartamentoNOMBRE_DEPARTAMENTO { get; set;}


    }
}