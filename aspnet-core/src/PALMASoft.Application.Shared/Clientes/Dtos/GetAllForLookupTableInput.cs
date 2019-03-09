using Abp.Application.Services.Dto;

namespace PALMASoft.Clientes.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}