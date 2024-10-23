using System.ComponentModel.DataAnnotations;

namespace Chirp.Razor;

public class CheepDTO
{
    [Required]
    public required string Text { get; set; }
    
    [Required]
    public required long Timestamp { get; set; }
    
    [Required]
    public required string Author { get; set; }  
}