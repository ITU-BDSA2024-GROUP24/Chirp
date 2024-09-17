namespace Chirp.CLI.Clint.Tests;

public class UnitTest1
{
    [Fact]
    public void ToStringTest()
    {
        Cheep cheep = new Cheep{ Author = "testAuthor", Message = "testMessage", Timestamp = 946684861 };
        String result = cheep.ToString();
        Assert.Equal("testAuthor @ 01-01-2000 01:01:01: testMessage", result);
    }
}