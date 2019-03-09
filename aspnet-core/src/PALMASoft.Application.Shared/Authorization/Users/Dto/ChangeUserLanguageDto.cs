using System.ComponentModel.DataAnnotations;

namespace PALMASoft.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
