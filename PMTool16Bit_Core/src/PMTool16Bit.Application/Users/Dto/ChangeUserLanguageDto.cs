using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Users
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}