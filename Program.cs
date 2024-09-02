﻿using System;
using System.Globalization;
using Chirp.CLI;
using CsvHelper;
using CsvHelper.Configuration;

string cheepDatabasePath = ".\\data\\chirp_cli_db.csv";
string operation = args[0];

switch (operation){
    case "read":
        readCheeps();
        break;
    case "cheep":
        makeCheep(args[1]);
        break;
    default:
        Console.WriteLine("Invalid operation");
        break;
}

void makeCheep(string message)
{
    long unixTimestamp = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
    var newCheep = new Cheep {Author = Environment.UserName, Message = message, Timestamp = unixTimestamp};
    using (var stream = File.Open(cheepDatabasePath, FileMode.Append))
    using (StreamWriter sw = new StreamWriter(stream))
    using (var csv = new CsvWriter(sw, CultureInfo.InvariantCulture))
    {
        csv.WriteRecord(newCheep);
        csv.NextRecord();

    }
}

 void readCheeps()
 {
     using (StreamReader sr = new StreamReader(cheepDatabasePath))
     using (var csv = new CsvReader(sr, CultureInfo.InvariantCulture))
     {
         var cheeps = csv.GetRecords<Cheep>();
         foreach (var cheep in cheeps)
         {
             Console.WriteLine(cheep);
         }
     }
 }