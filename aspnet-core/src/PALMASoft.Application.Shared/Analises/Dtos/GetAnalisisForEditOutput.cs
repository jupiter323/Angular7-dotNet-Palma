using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PALMASoft.Analises.Dtos
{
    public class GetAnalisisForEditOutput
    {
		public CreateOrEditAnalisisDto Analisis { get; set; }

		public string FincaNOMBRE_FINCA { get; set;}


    }
}