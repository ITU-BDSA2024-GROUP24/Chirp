using Chirp.Core;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;

public class CheepRepository : ICheepRepository
{
    
    private readonly ChirpDBContext _context;
    private const int cheepsPerPage = 32;

    public CheepRepository(ChirpDBContext context)
    {
        _context = context;
        context.Database.EnsureCreated();
    }
    
    public Task<Core.Author> GetAuthorByName(string name)
    {
        var query = (from author in _context.Authors where author.DisplayName == name select author);
        
        return query.FirstOrDefaultAsync()!;
    }
    
    public Task<Core.Author> GetAuthorByEmail(string email)
    {
        var query = (from author in _context.Authors where author.Email == email select author);
        
        return query.FirstOrDefaultAsync()!;
    }
    
    
    public async Task CreateCheep(Cheep newCheep)
    {
        _context.Add(newCheep);
        await _context.SaveChangesAsync();;
    }
    public async Task CreateAuthor(Core.Author newAuthor)
    {
        _context.Add(newAuthor);
        await _context.SaveChangesAsync();
    }

    public Task<List<CheepDTO>> ReadCheepDTO(int page)
    {
    {
        var query = (from cheep in _context.Cheeps
                orderby cheep.TimeStamp descending
                select new CheepDTO(){Text = cheep.Text, Timestamp = (long)cheep.TimeStamp.Subtract(DateTime.UnixEpoch).TotalSeconds, Author = cheep.Author.DisplayName})
            //.Include(c => c.Author)
            .Skip((page - 1) * cheepsPerPage)
            .Take(cheepsPerPage);
        return query.ToListAsync();
    }    }

    public Task UpdateCheep(CheepDTO alteredCheep)
    {
        throw new NotImplementedException();
    }
    
    public Task<List<CheepDTO>> ReadCheepDTOFromAuthor(int page, string authorName)
    {
        var query = (from cheep in _context.Cheeps
                where cheep.Author.DisplayName == authorName
                orderby cheep.TimeStamp descending
                select new CheepDTO(){Text = cheep.Text, Timestamp = (long)cheep.TimeStamp.Subtract(DateTime.UnixEpoch).TotalSeconds, Author = cheep.Author.DisplayName})
            //.Include(c => c.Author)
            .Skip((page - 1) * cheepsPerPage)
            .Take(cheepsPerPage);
        return query.ToListAsync();
    }
}