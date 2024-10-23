namespace Chirp.Razor;

public class CheepRepository : ICheepRepository
{
    
    private readonly ChirpDBContext _context;

    public CheepRepository(ChirpDBContext context)
    {
        _context = context;
        context.Database.EnsureCreated();
        DbInitializer.SeedDatabase(_context);
    }
    
    
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