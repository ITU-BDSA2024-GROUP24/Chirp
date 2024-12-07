using Chirp.Core;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure.ChirpRepositories;


public class CheepRepository : ICheepRepository
{
    
    private readonly ChirpDBContext _context;
    private const int cheepsPerPage = 32;

    public CheepRepository(ChirpDBContext context)
    {
        _context = context;
        context.Database.EnsureCreated();
    }
    
    
    // all create methods 
    public async Task CreateCheep(Cheep newCheep)
    {
        if (newCheep.Text.Length > 160) {
            throw new ArgumentException("Cheep text is too long");
        }
        else {
            _context.Add(newCheep);
            await _context.SaveChangesAsync();;  
        }
        
    }
    public async Task CreateAuthor(Author newAuthor)
    {
        _context.Add(newAuthor);
        await _context.SaveChangesAsync();
    }

    // all get methods
    public Task<Author> GetAuthorByName(string name)
    {
        var query = (from author in _context.Authors where author.UserName == name select author);
        
        return query.FirstOrDefaultAsync()!;
    }
    
    public Task<Author> GetAuthorByEmail(string email)
    {
        var query = (from author in _context.Authors where author.Email == email select author);
        
        return query.FirstOrDefaultAsync()!;
    }
    
    
    //read and update cheep methods 
    public Task<List<CheepDTO>> ReadCheepDTO(int page)
    {
    {
        var query = (from cheep in _context.Cheeps
                orderby cheep.TimeStamp descending
                select new CheepDTO(){Text = cheep.Text, Timestamp = (long)cheep.TimeStamp.Subtract(DateTime.UnixEpoch).TotalSeconds, Author = cheep.Author})
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
                where cheep.Author.UserName == authorName
                orderby cheep.TimeStamp descending
                select new CheepDTO(){Text = cheep.Text, Timestamp = (long)cheep.TimeStamp.Subtract(DateTime.UnixEpoch).TotalSeconds, Author = cheep.Author})
            //.Include(c => c.Author)
            .Skip((page - 1) * cheepsPerPage)
            .Take(cheepsPerPage);
        return query.ToListAsync();
    }
    
    
    
}