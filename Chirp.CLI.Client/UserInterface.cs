namespace Chirp.CLI;

static class UserInterface
{
    void PrintCheeps(IEnumerable<Cheep> cheeps)
    {
        var cheeps = csv.GetRecords<Cheep>();
        foreach (var cheep in cheeps)
        {
            Console.WriteLine(cheep);
        }
    }
        
}