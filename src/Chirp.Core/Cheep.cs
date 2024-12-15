using System.ComponentModel.DataAnnotations;

namespace Chirp.Core;

public class Cheep
{
    [Key]
    public  int CheepId { get; set; }
    
    
    [Required]
    public required Author Author { get; set; }
    
    [Required]
    [StringLength(160)]
    public required string Text { get; set; }
   
    [Required]
    public required DateTime TimeStamp { get; set; }
    


    //private ICollection<Author> _authors;

}