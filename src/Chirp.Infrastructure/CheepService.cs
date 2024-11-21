using Chirp.Core;
using Chirp.Infrastructure;
using Chirp.Razor;
using Author = Chirp.Core.Author;

public record CheepViewModel(string Author, string Message, string Timestamp);

public interface ICheepService
{
    List<CheepViewModel> GetCheeps(int page);
    List<CheepViewModel> GetCheepsFromAuthor(int page, string author);
    
    void AddCheep(Author author, string text);
}

public class CheepService : ICheepService
{
    private readonly ICheepRepository _repository;
    public CheepService(ICheepRepository repository)
    {
        _repository = repository; 
    }

    public List<CheepViewModel> GetCheeps(int page)
    {
        List<CheepDTO> cheepDTOs = _repository.ReadCheepDTO(page).Result;
        List<CheepViewModel> result = cheepDTOs.ConvertAll(cheep => new CheepViewModel(cheep.Author, cheep.Text, UnixTimeStampToDateTimeString(cheep.Timestamp)));
        return result;  // Call instance method
    }

    public List<CheepViewModel> GetCheepsFromAuthor(int page, string author)
    {
        List<CheepDTO> cheepDTOs = _repository.ReadCheepDTOFromAuthor(page, author).Result;
        List<CheepViewModel> result = cheepDTOs.ConvertAll(cheep => new CheepViewModel(cheep.Author, cheep.Text, UnixTimeStampToDateTimeString(cheep.Timestamp)));
        return result;  // Call instance method
    }
    private string UnixTimeStampToDateTimeString(long unixTimeStamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }
    
    public Author GetAuthorByName(string name)
    {
        Author author = _repository.GetAuthorByName(name).Result;
        return author;
    }
    
    public Author GetAuthorByEmail(string email)
    {
        Author author = _repository.GetAuthorByName(email).Result;
        return author;
    }

    public void AddCheep(Author author, string text)
    {
        Cheep cheep = new()
        {
            Author = author,
            Text = text,
            TimeStamp = DateTime.Now

        };
        _repository.CreateCheep(cheep);
    }

    public void AddAuthor(Author author)
    {
        _repository.CreateAuthor(author);

    }
}