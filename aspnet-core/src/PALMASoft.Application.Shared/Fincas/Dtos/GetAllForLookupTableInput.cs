using Abp.Application.Services.Dto;

namespace PALMASoft.Fincas.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}