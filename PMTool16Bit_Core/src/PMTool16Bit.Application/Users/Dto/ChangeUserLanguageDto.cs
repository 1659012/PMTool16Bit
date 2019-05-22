using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}