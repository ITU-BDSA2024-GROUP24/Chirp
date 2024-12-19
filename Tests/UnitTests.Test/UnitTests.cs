using Chirp.Core;
using Chirp.Infrastructure;
using Chirp.Infrastructure.ChirpRepositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace UnitTests.Test;

public class UnitTests
{

    private static async Task<ICheepRepository> SetUpRepositoryAsync()
    {
        var connection = new SqliteConnection("Filename=:memory:");
        await connection.OpenAsync();
        var builder = new DbContextOptionsBuilder<ChirpDBContext>().UseSqlite(connection);

        var context = new ChirpDBContext(builder.Options);
        await context.Database.EnsureCreatedAsync();

        return new CheepRepository(context);
    }
    
    private static Author newAuthor(string userName, string email)
    {
        Author author = new()
        {
            UserName = userName,
            Email = email,
            Cheeps = new List<Cheep>()
        };
        return author;
    }
    private static Cheep newCheep(Author author, string text)
    {
        Cheep cheep = new()
        {
            Author = author,
            Text = text,
            TimeStamp = DateTime.Now

        };
        return cheep;
    }

    [Fact]
    public async void RepositoryIsEmptyTest()
    {
        var repository = await SetUpRepositoryAsync();
        var cheeps = await repository.ReadCheepDTO(1);
        Assert.Empty(cheeps);
    }
   
    [Fact]
    public async void CreateAuthorAndGetAuthorByNameTest()
    {
        var repository = await SetUpRepositoryAsync();
        
        await repository.CreateAuthor(newAuthor("TestUser","test@email.com"));
        Assert.NotNull(await repository.GetAuthorByName("TestUser"));
    }
    
    [Fact]
    public async void GetAuthorByNameNotFoundTest()
    {
        var repository = await SetUpRepositoryAsync();
        var result = await repository.GetAuthorByName("TestUser");
        Assert.Null(result);
    }

    [Fact]
    public async void CreateCheepTest()
    {
        var repository = await SetUpRepositoryAsync();
        await repository.CreateAuthor(newAuthor("TestUser","test@email.com"));
        var testUser = await repository.GetAuthorByName("TestUser");
        await repository.CreateCheep(newCheep(testUser, "Hello World"));
        var cheeps = await repository.ReadCheepDTO(1);
        Assert.NotEmpty(cheeps);
    }

    [Fact]
    public async void EmptyPageTwoTest()
    {
        var repository = await SetUpRepositoryAsync();
        await repository.CreateAuthor(newAuthor("TestUser","test@email.com"));
        var testUser = await repository.GetAuthorByName("TestUser");
        await repository.CreateCheep(newCheep(testUser, "Hello World"));
        var cheeps = await repository.ReadCheepDTO(2);
        Assert.Empty(cheeps);
    }
    
    [Fact]
    public async void NotEmptyPageTwoTest()
    {
        var repository = await SetUpRepositoryAsync();
        await repository.CreateAuthor(newAuthor("TestUser","test@email.com"));
        var testUser = await repository.GetAuthorByName("TestUser");
        for (var i = 1; i <= 33; i++)
        {
            var text = ($"Hello World {i}"); 
            await repository.CreateCheep(newCheep(testUser, text));
        }
        await repository.CreateCheep(newCheep(testUser, "Hello World"));
        var cheeps = await repository.ReadCheepDTO(2);
        Assert.NotEmpty(cheeps);
    }
    
    /*
    Not yet tested functions from ICheepRepository:
    
    public Task<List<CheepDto>> ReadCheepDTOFromAuthor(int page, string authorName);
    public Task<Author> GetAuthorByEmail(string email);
    public Task<List<Cheep>> ReadCheepFromFollowed(string authorName);
    public Task<List<CheepDto>> ReadCheepDTOFromFollowed(List<Cheep> cheeps);
    */
}