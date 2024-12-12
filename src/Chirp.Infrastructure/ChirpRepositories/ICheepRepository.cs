using Chirp.Core;



namespace Chirp.Infrastructure.ChirpRepositories;

public interface ICheepRepository
{
    public Task CreateCheep(Cheep newCheep);
    public Task CreateAuthor(Author newCheep);
    public Task<List<CheepDTO>> ReadCheepDTO(int page);
    public Task<List<CheepDTO>> ReadCheepDTOFromAuthor(int page, string authorName);
    public Task UpdateCheep(CheepDTO alteredCheep);
    public Task<Core.Author> GetAuthorByName(string name);
    public Task<Core.Author> GetAuthorByEmail(string email);
    public Task<List<Cheep>> ReadCheepFromFollowed(string authorName);
    public Task<List<CheepDTO>> ReadCheepDTOFromFollowed(List<Cheep> cheeps);

}                                   