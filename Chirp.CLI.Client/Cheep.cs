namespace Chirp.CLI;

public class Cheep
{
    public required string Author { get; set; }
    public required string Message { get; set; }
    public long Timestamp { get; set; }

    public override string ToString()
    {
        DateTime date = DateTimeOffset.FromUnixTimeSeconds(Timestamp).LocalDateTime;
        return $"{Author} @ {date.ToString("dd/MM/yyyy HH\\:mm\\:ss")}: {Message}";
    }
}