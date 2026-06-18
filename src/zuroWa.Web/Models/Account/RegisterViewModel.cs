using System.ComponentModel.DataAnnotations;

namespace zuroWa.Web.Models.Account;

public class RegisterViewModel
{
    [Required] [MinLength(4)] public string Username { get; set; } = string.Empty;

    [Required] [MinLength(6)] public string Password { get; set; } = string.Empty;

    [Required]
    [Compare(nameof(Password), ErrorMessage = "Must match Password")]
    public string ConfirmPassword { get; set; } = string.Empty;
}