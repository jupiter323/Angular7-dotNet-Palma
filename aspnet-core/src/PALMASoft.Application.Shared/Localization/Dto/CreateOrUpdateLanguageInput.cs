using System.ComponentModel.DataAnnotations;

namespace PALMASoft.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}