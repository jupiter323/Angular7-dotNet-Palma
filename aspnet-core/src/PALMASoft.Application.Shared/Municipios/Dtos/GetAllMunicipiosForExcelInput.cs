using Abp.Application.Services.Dto;
using System;

namespace PALMASoft.Municipios.Dtos
{
    public class GetAllMunicipiosForExcelInput
    {
		public string Filter { get; set; }

		public string ID_MUNICIPIOFilter { get; set; }

		public string NOMBRE_MUNICIPIOFilter { get; set; }


		 public string DepartamentoNOMBRE_DEPARTAMENTOFilter { get; set; }

		 
    }
}