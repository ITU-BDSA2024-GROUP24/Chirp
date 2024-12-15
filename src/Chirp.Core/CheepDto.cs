using System.ComponentModel.DataAnnotations;

namespace Chirp.Core;

public class CheepDto
{
    [Required]
    public required string Text { get; set; }
    
    [Required]
    public required long Timestamp { get; set; }
    
    [Required]
    public required Author Author { get; set; }  

}