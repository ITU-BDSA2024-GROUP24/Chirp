namespace Chirp.Razor;

public class Cheep
{
    public required int CheepId { get; set; }
    
    public required int AuthorId { get; set; }
    public required Author Author { get; set; }
    public required string Text { get; set; }
    public required DateTime TimeStamp { get; set; }
    

    //private ICollection<Author> _authors;

   /* public Cheep(string Text, DateTime TimeStamp)
    {
        this.Text = Text;
        this.TimeStamp = TimeStamp;
        
    }*/

}