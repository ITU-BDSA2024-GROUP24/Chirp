using Chirp.Core;

namespace Chirp.Infrastructure;

public interface ICheepRepository
{


    public Task CreateCheep(Cheep newCheep);
    public Task CreateAuthor(Author newCheep);
    public Task<List<CheepDTO>> ReadCheepDTO(int page);
    public Task<List<CheepDTO>> ReadCheepDTOFromAuthor(int page, string authorName);
    public Task UpdateCheep(CheepDTO alteredCheep);
    public Task<Author> GetAuthorByName(string name);
    public Task<Author> GetAuthorByEmail(string email);
}                                   