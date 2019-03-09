using Abp.Application.Services.Dto;
using System;

namespace PALMASoft.Clientes.Dtos
{
    public class GetAllClientesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string ID_CLIENTEFilter { get; set; }

		public string NOMBRE_CLIENTEFilter { get; set; }

		public string APELLIDO_CLIENTEFilter { get; set; }

		public int GENERO_CLIENTEFilter { get; set; }

		public DateTime? MaxFECHA_CLIENTEFilter { get; set; }
		public DateTime? MinFECHA_CLIENTEFilter { get; set; }

		public string CELULAR_CLIENTEFilter { get; set; }

		public string DIRECCION_CLIENTEFilter { get; set; }

		public string DEPARTAMENTO_CLIENTEFilter { get; set; }

		public string MUNICIPIO_CLIENTEFilter { get; set; }

		public string EMPRESA_CLIENTEFilter { get; set; }

		public string PROFESION_CLIENTEFilter { get; set; }



    }
}