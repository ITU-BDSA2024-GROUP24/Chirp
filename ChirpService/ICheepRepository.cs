namespace Chirp.Razor;

public interface ICheepRepository
{


    public Task CreateCheep(CheepDTO newCheep);
    public Task<List<CheepDTO>> ReadCheep(string userName);
    public Task UpdateCheep(CheepDTO alteredCheep);
}                                   