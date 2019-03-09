using Abp.Application.Services.Dto;

namespace PALMASoft.Paises.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}