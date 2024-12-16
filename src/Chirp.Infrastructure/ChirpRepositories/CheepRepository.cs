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
    public Task<List<CheepDto>> ReadCheepDTO(int page)
    {
    {
        var query = (from cheep in _context.Cheeps
                orderby cheep.TimeStamp descending
                select new CheepDto(){Text = cheep.Text, Timestamp = (long)cheep.TimeStamp.Subtract(DateTime.UnixEpoch).TotalSeconds, Author = cheep.Author})
            //.Include(c => c.Author)
            .Skip((page - 1) * cheepsPerPage)
            .Take(cheepsPerPage);
        return query.ToListAsync();
    }    }

    public Task UpdateCheep(CheepDto alteredCheep)
    {
        throw new NotImplementedException();
    }
    
    public Task<List<CheepDto>> ReadCheepDTOFromAuthor(int page, string authorName)
    {
        var query = (from cheep in _context.Cheeps
                where cheep.Author.UserName == authorName
                orderby cheep.TimeStamp descending
                select new CheepDto(){Text = cheep.Text, Timestamp = (long)cheep.TimeStamp.Subtract(DateTime.UnixEpoch).TotalSeconds, Author = cheep.Author})
            //.Include(c => c.Author)
            .Skip((page - 1) * cheepsPerPage)
            .Take(cheepsPerPage);
        return query.ToListAsync();
    }

    public Task<List<Cheep>> ReadCheepFromFollowed(string authorName)
    {
        var query = (from cheep in _context.Cheeps
            where (from follow in _context.Followers
                where follow.FollowedBy == authorName
                select follow.FollowThem).Contains(cheep.Author.UserName)
            select cheep).Include(cheep => cheep.Author);
        
        return query.ToListAsync();
       
    }
    public Task<List<CheepDto>> ReadCheepDTOFromFollowed(List<Cheep> cheeps)
    {
        var CheepDTOs = new List<CheepDto>();
        foreach (var cheep in cheeps)
        {
            var DTO = new CheepDto
            {
                Text = cheep.Text,
                Author = cheep.Author,
                Timestamp = (long)cheep.TimeStamp.Subtract(DateTime.UnixEpoch).TotalSeconds,
            };
            CheepDTOs.Add(DTO);
        } 
        return Task.FromResult(CheepDTOs);
    }


}