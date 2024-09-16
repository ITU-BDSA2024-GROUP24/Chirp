namespace Chirp.CLI;
static class UserInterface
{
    public static void printCheeps(IEnumerable<Cheep> cheeps)
    {
        foreach (var cheep in cheeps)
        {
            Console.WriteLine(cheep);
        }
    }
}