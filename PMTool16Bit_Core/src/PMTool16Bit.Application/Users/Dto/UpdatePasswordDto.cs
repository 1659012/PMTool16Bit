using System.ComponentModel.DataAnnotations;

namespace PMTool16Bit.Users
{
    public class UpdatePasswordDto
    {
        [Required]
        public long UserId { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [Required]
        public string NewPassword { get; set; }
    }
}
