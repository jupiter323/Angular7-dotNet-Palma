using PALMASoft.Clientes;

using System;
using Abp.Application.Services.Dto;

namespace PALMASoft.Clientes.Dtos
{
    public class ClienteDto : EntityDto<long>
    {
		public string ID_CLIENTE { get; set; }

		public string NOMBRE_CLIENTE { get; set; }

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