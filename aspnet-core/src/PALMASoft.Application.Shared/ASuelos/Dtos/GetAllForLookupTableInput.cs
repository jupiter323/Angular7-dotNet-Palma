using Abp.Application.Services.Dto;

namespace PALMASoft.ASuelos.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}