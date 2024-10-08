namespace Chirp.Razor;

public class Author
{
    public string Name { get; set; }
    public string Email { get; set; }
    
    private ICollection<Cheep> _cheeps;     
}