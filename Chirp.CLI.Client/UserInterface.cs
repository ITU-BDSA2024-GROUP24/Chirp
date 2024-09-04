namespace Chirp.CLI;

static class UserInterface
{
    void PrintCheeps(IEnumerable<Cheep> cheeps)
    {
        foreach (var cheep in cheeps)
        {
            Console.WriteLine(cheep);
        }
    }
        
}