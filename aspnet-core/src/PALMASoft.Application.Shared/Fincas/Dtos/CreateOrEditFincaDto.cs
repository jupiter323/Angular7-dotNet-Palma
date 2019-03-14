
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PALMASoft.Fincas.Dtos
{
    public class CreateOrEditFincaDto : EntityDto<long?>
    {

		[Required]
		[StringLength(FincaConsts.MaxID_FINCALength, MinimumLength = FincaConsts.MinID_FINCALength)]
		public string ID_FINCA { get; set; }
		
		
		[StringLength(FincaConsts.MaxNOMBRE_FINCALength, MinimumLength = FincaConsts.MinNOMBRE_FINCALength)]
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