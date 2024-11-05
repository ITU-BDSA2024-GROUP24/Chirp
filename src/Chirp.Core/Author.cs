using System.ComponentModel.DataAnnotations;

namespace Chirp.Core;

public class Author
{ 
    [Required] 
    public required int AuthorId { get; set; }
    
    [Required] 
    public required string Name { get; set; }
    
    
    [Required] 
    public required string Email { get; set; }
    
    [Required]
    public required ICollection<Cheep> Cheeps;
    
}