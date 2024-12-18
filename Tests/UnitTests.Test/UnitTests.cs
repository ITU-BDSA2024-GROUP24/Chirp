using Chirp.Core;
using Chirp.Infrastructure;
using Chirp.Infrastructure.ChirpRepositories;
using Chirp.Infrastructure.ChirpServices;
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
    public async void CreateAuthorTest()
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
    
}