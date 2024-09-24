using System.Globalization;
using Chirp.CLI;
using CsvHelper;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var cheeps = new List<Cheep>();
string recordDatabasePath = "..\\..\\data\\chirp_cli_db.csv";

app.MapGet("/cheeps", () => Read());

app.MapPut("/cheep", (Cheep cheep) => Store(cheep));


app.Run();


List<Cheep> Read(int? limit = null) {
    using (StreamReader sr = new StreamReader(recordDatabasePath))
    using (var csv = new CsvReader(sr, CultureInfo.InvariantCulture))
    {
        return csv.GetRecords<Cheep>().ToList();
    }
}

void Store(Cheep record) {
    using (StreamWriter sw = new StreamWriter(recordDatabasePath, append: true))
    using (var csv = new CsvWriter(sw, CultureInfo.InvariantCulture))
    {
        csv.WriteRecord(record);
        csv.NextRecord();
    }
} 








        