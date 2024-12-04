using System.ComponentModel.DataAnnotations;

namespace Chirp.Web.Pages.Shared;

public class CheepFormatMessage
{
    [Required]
    [StringLength(160, ErrorMessage = "Maximum length is 160 characters.")]
    [Display(Name = "Message")]
    public string Message { get; set; } = string.Empty;
}