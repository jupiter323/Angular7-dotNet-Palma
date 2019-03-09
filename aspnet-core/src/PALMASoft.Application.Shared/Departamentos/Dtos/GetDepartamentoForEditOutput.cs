using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PALMASoft.Departamentos.Dtos
{
    public class GetDepartamentoForEditOutput
    {
		public CreateOrEditDepartamentoDto Departamento { get; set; }

		public string PaisNOMBRE_PAIS { get; set;}


    }
}