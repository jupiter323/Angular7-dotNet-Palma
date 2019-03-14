using Abp.Application.Services.Dto;
using System;

namespace PALMASoft.Fincas.Dtos
{
    public class GetAllFincasInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string ID_FINCAFilter { get; set; }

		public string NOMBRE_FINCAFilter { get; set; }

		public string DEPARTAMENTO_FINCAFilter { get; set; }

		public string MUNICIPIO_FINCAFilter { get; set; }

		public string VEREDA_FINCAFilter { get; set; }

		public string CORREGIMIENTO_FINCAFilter { get; set; }

		public string UBICACION_FINCAFilter { get; set; }

		public string LONGITUD_FINCAFilter { get; set; }

		public string LATITUD_FINCAFilter { get; set; }

		public string CONTACTO_FINCAFilter { get; set; }

		public string TELEFONO_FINCAFilter { get; set; }

		public string CORREO_FINCAFilter { get; set; }


		 public string ClienteNOMBRE_CLIENTEFilter { get; set; }

		 
    }
}