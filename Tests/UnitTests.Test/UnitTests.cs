using Chirp.Core;
using Chirp.Infrastructure;
using Chirp.Infrastructure.ChirpRepositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class CheepRepositoryTest
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

    [Fact]
    public async void CheckThatRepositoryIsEmptyTest()
    {
        var repository = await SetUpRepositoryAsync();
        var result = await repository.ReadCheepDTO(1);
        Assert.Empty(result);
    }
   
}