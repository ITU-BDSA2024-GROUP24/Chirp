using Chirp.Razor;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    List<CheepViewModel> GetCheeps();
    List<CheepViewModel> GetCheepsFromAuthor(string author);
}

public class CheepService : ICheepService
{
    private readonly DbFacade _dbFacade;                            

    public CheepService()
    {
        _dbFacade = new DbFacade(); 
    }

    public List<CheepViewModel> GetCheeps()
    {
        return _dbFacade.GetCheeps();  // Call instance method
    }

    public List<CheepViewModel> GetCheepsFromAuthor(string author)
    {
        return _dbFacade.GetCheepsFromAuthor(author);  // Call instance method
    }
}