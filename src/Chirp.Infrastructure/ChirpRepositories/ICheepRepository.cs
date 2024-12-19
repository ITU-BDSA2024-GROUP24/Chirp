using Chirp.Core;



namespace Chirp.Infrastructure.ChirpRepositories;

public interface ICheepRepository
{
    public Task CreateCheep(Cheep newCheep);
    public Task CreateAuthor(Author newCheep);
    public Task<List<CheepDto>> ReadCheepDTO(int page);
    public Task<List<CheepDto>> ReadCheepDTOFromAuthor(int page, string authorName);
    public Task UpdateCheep(CheepDto alteredCheep);
    public Task<Author> GetAuthorByName(string name);
    public Task<Author> GetAuthorByEmail(string email);
    public Task<List<Cheep>> ReadCheepFromFollowed(string authorName);
    public Task<List<CheepDto>> ReadCheepDTOFromFollowed(List<Cheep> cheeps);

}                                   