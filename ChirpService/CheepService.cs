using Chirp.Razor;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    List<CheepViewModel> GetCheeps(int skip);
    List<CheepViewModel> GetCheepsFromAuthor(string author, int skip);
}

public class CheepService : ICheepService
{
    private readonly DbFacade _dbFacade;                            

    public CheepService()
    {
        _dbFacade = new DbFacade(); 
    }

    public List<CheepViewModel> GetCheeps(int skip)
    {
        return _dbFacade.GetCheeps (skip);  // Call instance method
    }

    public List<CheepViewModel> GetCheepsFromAuthor(string author, int skip)
    {
        return _dbFacade.GetCheepsFromAuthor(author, skip);  // Call instance method
    }
}