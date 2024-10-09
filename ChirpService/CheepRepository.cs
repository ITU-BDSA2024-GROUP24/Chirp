namespace Chirp.Razor;

public class CheepRepository : ICheepRepository
{
    public Task CreateCheep(CheepDTO newCheep)
    {
        throw new NotImplementedException();
    }

    public Task<List<CheepDTO>> ReadCheep(string userName)
    {
        throw new NotImplementedException();
    }

    public Task UpdateCheep(CheepDTO alteredCheep)
    {
        throw new NotImplementedException();
    }
}