using Chirp.Core;

namespace Chirp.Infrastructure;

public interface ICheepRepository
{
    public Task CreateCheep(Cheep newCheep);
    public Task CreateAuthor(Core.Author newCheep);
    public Task<List<CheepDTO>> ReadCheepDTO(int page);
    public Task<List<CheepDTO>> ReadCheepDTOFromAuthor(int page, string authorName);
    public Task UpdateCheep(CheepDTO alteredCheep);
    public Task<Core.Author> GetAuthorByName(string name);
    public Task<Core.Author> GetAuthorByEmail(string email);
    
}                                   