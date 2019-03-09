using Abp.Application.Services.Dto;

namespace PALMASoft.Departamentos.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}