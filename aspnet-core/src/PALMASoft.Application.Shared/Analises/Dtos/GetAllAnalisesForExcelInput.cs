using Abp.Application.Services.Dto;
using System;

namespace PALMASoft.Analises.Dtos
{
    public class GetAllAnalisesForExcelInput
    {
		public string Filter { get; set; }

		public string ID_INFORMEFilter { get; set; }

		public int TIPO_INFORMEFilter { get; set; }

		public DateTime? MaxFECHA_MUESTREOFilter { get; set; }
		public DateTime? MinFECHA_MUESTREOFilter { get; set; }

		public DateTime? MaxFECHA_REGISTROFilter { get; set; }
		public DateTime? MinFECHA_REGISTROFilter { get; set; }

		public DateTime? MaxFECHA_ENTREGAFilter { get; set; }
		public DateTime? MinFECHA_ENTREGAFilter { get; set; }


		 public string FincaNOMBRE_FINCAFilter { get; set; }

		 
    }
}