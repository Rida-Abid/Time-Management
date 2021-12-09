using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace TTMS.Models
{
    [Bind("EmailAddress,Password")]
    public class AccountViewModel
    {
        [Required]
        [StringLength(20, ErrorMessage = "Email address length can't be more than 20.")]
        public string? EmailAddress { get; set; }

        [Required]
        [StringLength(20, ErrorMessage = "Password length can't be more than 20.")]
        public string? Password { get; set; }

    }
}
