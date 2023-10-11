using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace TalabatApi.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]

        public string Email { get; set; }

        [Required]

        public string Password { get; set; }

        [Required]

        public string PhoneNumber { get; set; }
    }
}
