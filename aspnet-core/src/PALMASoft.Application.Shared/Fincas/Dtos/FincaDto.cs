
using System;
using Abp.Application.Services.Dto;

namespace PALMASoft.Fincas.Dtos
{
    public class FincaDto : EntityDto<long>
    {
		public string ID_FINCA { get; set; }

		public string NOMBRE_FINCA { get; set; }

		public string DEPARTAMENTO_FINCA { get; set; }

		public string MUNICIPIO_FINCA { get; set; }

		public string VEREDA_FINCA { get; set; }

		public string CORREGIMIENTO_FINCA { get; set; }

		public string UBICACION_FINCA { get; set; }

		public string LONGITUD_FINCA { get; set; }

		public string LATITUD_FINCA { get; set; }

		public string CONTACTO_FINCA { get; set; }

		public string TELEFONO_FINCA { get; set; }

		public string CORREO_FINCA { get; set; }


		 public long? ClienteId { get; set; }

		 
    }
}