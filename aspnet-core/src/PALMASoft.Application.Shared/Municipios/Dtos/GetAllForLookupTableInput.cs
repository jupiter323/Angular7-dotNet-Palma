using Abp.Application.Services.Dto;

namespace PALMASoft.Municipios.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}