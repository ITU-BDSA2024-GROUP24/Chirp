using Chirp.Core;
using Chirp.Infrastructure.ChirpRepositories;
using Author = Chirp.Core.Author;

namespace Chirp.Infrastructure.ChirpServices;
public record CheepViewModel(string? Author, string Message, string Timestamp);

public interface ICheepService
{
    List<CheepViewModel> GetCheeps(int page);
    List<CheepViewModel> GetCheepsFromAuthor(int page, string author);
    Task AddCheep(Author author, string text);
    Task<Author> GetAuthorByName(string name);
    Task<Author> GetAuthorByEmail(string email);
    Task AddAuthor(Author author);
    //Task AddFollower(string followerUser, string followedUser);
    List<CheepViewModel> GetCheepsFromFollower(int page, string author);
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
        List<CheepDto> cheepDtos = _repository.ReadCheepDTO(page).Result;
        List<CheepViewModel> result = cheepDtos.ConvertAll(cheep =>
            new CheepViewModel(cheep.Author.UserName, cheep.Text, UnixTimeStampToDateTimeString(cheep.Timestamp)));
        return result;
    }

    public List<CheepViewModel> GetCheepsFromAuthor(int page, string author)
    {
        List<CheepDto> cheepDtos = _repository.ReadCheepDTOFromAuthor(page, author).Result;
        List<CheepViewModel> result = cheepDtos.ConvertAll(cheep =>
            new CheepViewModel(cheep.Author.UserName, cheep.Text, UnixTimeStampToDateTimeString(cheep.Timestamp)));
        return result;
    }

    

    public List<CheepViewModel> GetCheepsFromFollower(int page, string author)
    {
        List<Cheep> cheeps = _repository.ReadCheepFromFollowed(author).Result;
        List<CheepDto> cheepDtos = _repository.ReadCheepDTOFromFollowed(cheeps).Result;
        List<CheepViewModel> result = cheepDtos.ConvertAll(cheep =>
            new CheepViewModel(cheep.Author.UserName, cheep.Text, UnixTimeStampToDateTimeString(cheep.Timestamp)));
        return result;
    }


    private string UnixTimeStampToDateTimeString(long unixTimeStamp)
    {
        DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTime = dateTime.AddSeconds(unixTimeStamp);
        return dateTime.ToString("MM/dd/yy H:mm:ss");
    }

    public async Task<Author> GetAuthorByName(string name)
    {
        return await _repository.GetAuthorByName(name);
    }

    public async Task<Author> GetAuthorByEmail(string email)
    {
        return await _repository.GetAuthorByEmail(email);
    }

    public async Task AddCheep(Author author, string text)
    {
        Cheep cheep = new()
        {
            Author = author,
            Text = text,
            TimeStamp = DateTime.Now

        };
        await _repository.CreateCheep(cheep);
    }

    public async Task AddAuthor(Author author)
    {
        await _repository.CreateAuthor(author);
    }

    //public Task AddFollower(string followerUser, string followedUser)
}


   