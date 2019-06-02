using System.ComponentModel.DataAnnotations;


namespace PMTool16Bit.Services
{
    public class ChangePasswordViewModel
    {
        [Required]
        public string OldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
