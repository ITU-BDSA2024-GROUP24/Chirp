namespace Chirp.Razor;

public class Author
{
    public required int AuthorId { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    
    public ICollection<Cheep> Cheeps;

   /* public Author (string name, string email)
    {
        this.Name = name;
        this.Email = email;
    }*/
    
    
}