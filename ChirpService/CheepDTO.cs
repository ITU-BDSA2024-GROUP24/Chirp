namespace Chirp.Razor;

public class CheepDTO
{
   required public string Text { get; set; }
    
    public long Timestamp { get; set; }
    
   required public string Author { get; set; }  
}