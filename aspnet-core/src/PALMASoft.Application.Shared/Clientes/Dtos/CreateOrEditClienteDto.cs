using PALMASoft.Clientes;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace PALMASoft.Clientes.Dtos
{
    public class CreateOrEditClienteDto : EntityDto<long?>
    {

		[Required]
		[StringLength(ClienteConsts.MaxID_CLIENTELength, MinimumLength = ClienteConsts.MinID_CLIENTELength)]
		public string ID_CLIENTE { get; set; }
		
		
		[StringLength(ClienteConsts.MaxNOMBRE_CLIENTELength, MinimumLength = ClienteConsts.MinNOMBRE_CLIENTELength)]
		public string NOMBRE_CLIENTE { get; set; }
		
		
		[StringLength(ClienteConsts.MaxAPELLIDO_CLIENTELength, MinimumLength = ClienteConsts.MinAPELLIDO_CLIENTELength)]
		public string APELLIDO_CLIENTE { get; set; }
		
		
		public Generos GENERO_CLIENTE { get; set; }
		
		
		public DateTime? FECHA_CLIENTE { get; set; }
		
		
		public string CELULAR_CLIENTE { get; set; }
		
		
		public string DIRECCION_CLIENTE { get; set; }
		
		
		public string DEPARTAMENTO_CLIENTE { get; set; }
		
		
		public string MUNICIPIO_CLIENTE { get; set; }
		
		
		public string EMPRESA_CLIENTE { get; set; }
		
		
		public string PROFESION_CLIENTE { get; set; }
		
		

    }
}