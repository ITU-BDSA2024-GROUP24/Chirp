namespace Chirp.Razor;

public class Cheep
{
    public int CheepId { get; set; }
    
    public int AuthorId { get; set; }
    public Author Author { get; set; }
    public string Text { get; set; }
    public DateTime TimeStamp { get; set; }
    

    //private ICollection<Author> _authors;

   /* public Cheep(string Text, DateTime TimeStamp)
    {
        this.Text = Text;
        this.TimeStamp = TimeStamp;
        
    }*/

}