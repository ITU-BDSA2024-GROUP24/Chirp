using System;
using System.Globalization;
using Chirp.CLI;
using CsvHelper;
using CsvHelper.Configuration;
using SimpleDB;

string operation = args[0];

var db = new CSVDatabase<Cheep>();

switch (operation){
    case "read":
        var cheeps = db.Read();
        break;
    case "cheep":
        db.Store(makeCheep(args[1]));
        break;
    default:
        Console.WriteLine("Invalid operation");
        break;
}
// Source: https://code-maze.com/csharp-writing-csv-file/
Cheep makeCheep(string message)
{
    long unixTimestamp = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
    return new Cheep {Author = Environment.UserName, Message = message, Timestamp = unixTimestamp};
}
// Source: https://code-maze.com/csharp-read-data-from-csv-file/

