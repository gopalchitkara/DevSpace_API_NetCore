using System.ComponentModel.DataAnnotations;

namespace DevSpace_API.Shared
{
    public class SignInModel
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 5)]
        public string Password { get; set; }

    }
}