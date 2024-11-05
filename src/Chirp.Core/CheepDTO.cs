using System.ComponentModel.DataAnnotations;

namespace Chirp.Core;

public class CheepDTO
{
    [Required]
    public required string Text { get; set; }

    
    [Required]
    public required long Timestamp { get; set; }
    
    [Required]
    public required string Author { get; set; }  

}