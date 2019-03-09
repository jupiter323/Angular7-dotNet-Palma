using Abp.Application.Services.Dto;
using System;

namespace PALMASoft.Departamentos.Dtos
{
    public class GetAllDepartamentosForExcelInput
    {
		public string Filter { get; set; }

		public string ID_DEPARTAMENTOFilter { get; set; }

		public string NOMBRE_DEPARTAMENTOFilter { get; set; }


		 public string PaisNOMBRE_PAISFilter { get; set; }

		 
    }
}