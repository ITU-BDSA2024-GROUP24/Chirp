using Chirp.CLI;
using DocoptNet;
using SimpleDB;

var operation = args[0];

const string usage = @"Chirp CLI version.

Usage:
  chirp read  
  chirp cheep <message>
  chirp (-h | --help)
  chirp --version

Options:
  -h --help     Show this screen.
  --version     Show version.
";
var arguments = new Docopt().Apply(usage, args, version: "1.0", exit: true)!;
var db = new CSVDatabase<Cheep>();

if (arguments["read"].IsTrue)
{
    var cheeps = db.Read();
    UserInterface.printCheeps(cheeps);
}
else if (arguments["cheep"].IsTrue)
{
    db.Store(makeCheep(arguments["<message>"].ToString()));
}
else
{
    Console.WriteLine("Invalid operation");
}

/*
switch (arguments["<message>"].ToString())
{
    case "read":
        var cheeps = db.Read();
        UserInterface.printCheeps(cheeps);
        break;
    case "cheep":
        break;
}
*/
Cheep makeCheep(string message)
{
    var unixTimestamp = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
    return new Cheep { Author = Environment.UserName, Message = message, Timestamp = unixTimestamp };
}