using System.ComponentModel.DataAnnotations;

namespace Chirp.Razor;

public class Author
{ 
    [Required] 
    public  int AuthorId { get; set; }
    [Required] 
    public required string Name { get; set; }
    [Required] 
    public required string Email { get; set; }
    [Required]
    public ICollection<Cheep> Cheeps;

   /* public Author (string name, string email)
    {
        this.Name = name;
        this.Email = email;
    }*/
    
    
}