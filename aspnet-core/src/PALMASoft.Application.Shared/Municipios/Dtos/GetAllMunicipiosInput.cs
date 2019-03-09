using Abp.Application.Services.Dto;
using System;

namespace PALMASoft.Municipios.Dtos
{
    public class GetAllMunicipiosInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string ID_MUNICIPIOFilter { get; set; }

		public string NOMBRE_MUNICIPIOFilter { get; set; }


		 public string DepartamentoNOMBRE_DEPARTAMENTOFilter { get; set; }

		 
    }
}