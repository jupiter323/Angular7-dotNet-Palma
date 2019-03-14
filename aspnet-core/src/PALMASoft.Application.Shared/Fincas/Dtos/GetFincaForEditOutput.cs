using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PALMASoft.Fincas.Dtos
{
    public class GetFincaForEditOutput
    {
		public CreateOrEditFincaDto Finca { get; set; }

		public string ClienteNOMBRE_CLIENTE { get; set;}


    }
}