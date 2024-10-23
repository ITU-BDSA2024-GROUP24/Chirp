using System.ComponentModel.DataAnnotations;

namespace Chirp.Razor;

public class Cheep
{
    [Required]
    public int CheepId { get; set; }
    [Required]
    public int AuthorId { get; set; }
    [Required]
    public Author Author { get; set; }
    
    [Required]
    [StringLength(160)]
    public string Text { get; set; }
    [Required]
    public DateTime TimeStamp { get; set; }
    

    //private ICollection<Author> _authors;

   /* public Cheep(string Text, DateTime TimeStamp)
    {
        this.Text = Text;
        this.TimeStamp = TimeStamp;
        
    }*/

}