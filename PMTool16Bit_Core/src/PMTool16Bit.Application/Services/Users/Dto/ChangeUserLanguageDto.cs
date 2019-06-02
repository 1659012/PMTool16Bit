using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Services
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}