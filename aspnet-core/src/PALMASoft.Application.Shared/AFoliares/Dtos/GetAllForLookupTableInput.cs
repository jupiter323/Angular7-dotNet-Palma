using Abp.Application.Services.Dto;

namespace PALMASoft.AFoliares.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}