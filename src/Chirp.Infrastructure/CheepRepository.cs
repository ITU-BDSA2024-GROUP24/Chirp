using Chirp.Core;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Infrastructure;

public interface ICheepRepository
{
    public Task CreateCheep(Cheep newCheep);
    public Task CreateAuthor(Author newCheep);
    public Task<List<CheepDTO>> ReadCheepDTO(int page);
    public Task<List<CheepDTO>> ReadCheepDTOFromAuthor(int page, string authorName);
    //public Task UpdateCheep(CheepDTO alteredCheep);
    public Task<Author> GetAuthorByName(string name);
    public Task<Author> GetAuthorByEmail(string email);
}  

public class CheepRepository : ICheepRepository
{
    private readonly ChirpDBContext _context;
    private const int cheepsPerPage = 32;

    public CheepRepository(ChirpDBContext context)
    {
        _context = context;
        context.Database.EnsureCreated();
    }
    
    public Task<Author> GetAuthorByName(string name)
    {
        var query = (from author in _context.Authors where author.Name == name select author);
        
        return query.FirstOrDefaultAsync()!;
    }
    
    // this method is a nullable type, it is used in create a cheep to check if the Author exists or not. 
    public Task<Author?> FindAuthorByName(string name)
    {
        return _context.Authors.FirstOrDefaultAsync(a => a.Name == name);
    }
    
    public Task<Author> GetAuthorByEmail(string email)
    {
        var query = (from author in _context.Authors where author.Email == email select author);
        
        return query.FirstOrDefaultAsync()!;
    }
    
    // add to existing code so: 'Note, that might mean to also create a respective author if she does not exist yet in Chirp!.' 
    public async Task CreateCheep(Cheep newCheep)
    {
        var authorExisting = await FindAuthorByName(newCheep.Author.Name);

        if (authorExisting == null)
        {
            await CreateAuthor(newCheep.Author);
        }
        else
        {
            newCheep.Author = authorExisting;
            newCheep.AuthorId = authorExisting.AuthorId;
        }
        
        _context.Add(newCheep);
        await _context.SaveChangesAsync();
    }
    
    public async Task CreateAuthor(Author newAuthor)
    {
        _context.Add(newAuthor);
        await _context.SaveChangesAsync();
    }

    public Task<List<CheepDTO>> ReadCheepDTO(int page)
    {
    {
        var query = (from cheep in _context.Cheeps
                orderby cheep.TimeStamp descending
                select new CheepDTO(){Text = cheep.Text, Timestamp = (long)cheep.TimeStamp.Subtract(DateTime.UnixEpoch).TotalSeconds, Author = cheep.Author.Name})
            //.Include(c => c.Author)
            .Skip((page - 1) * cheepsPerPage)
            .Take(cheepsPerPage);
        return query.ToListAsync();
    }    }

   /* public Task UpdateCheep(CheepDTO alteredCheep)
    {
        throw new NotImplementedException();
    }*/
    
    public Task<List<CheepDTO>> ReadCheepDTOFromAuthor(int page, string authorName)
    {
        var query = (from cheep in _context.Cheeps
                where cheep.Author.Name == authorName
                orderby cheep.TimeStamp descending
                select new CheepDTO(){Text = cheep.Text, Timestamp = (long)cheep.TimeStamp.Subtract(DateTime.UnixEpoch).TotalSeconds, Author = cheep.Author.Name})
            //.Include(c => c.Author)
            .Skip((page - 1) * cheepsPerPage)
            .Take(cheepsPerPage);
        return query.ToListAsync();
    }
}