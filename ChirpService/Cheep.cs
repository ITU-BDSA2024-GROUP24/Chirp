namespace Chirp.Razor;

public class Cheep
{
    public string Text { get; set; }
    public DateTime Timestamp { get; set; }

    private ICollection<Author> _authors;

}