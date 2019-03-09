using Abp.AutoMapper;
using PALMASoft.Organizations.Dto;

namespace PALMASoft.Models.Users
{
    [AutoMapFrom(typeof(OrganizationUnitDto))]
    public class OrganizationUnitModel : OrganizationUnitDto
    {
        public bool IsAssigned { get; set; }
    }
}