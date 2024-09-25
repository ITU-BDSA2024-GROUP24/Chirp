using Chirp.CLI;
using DocoptNet;
using SimpleDB;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;


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

// Create an HTTP client object
var baseURL = "http://localhost:5144";
using HttpClient client = new();
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
client.BaseAddress = new Uri(baseURL);

// Send an asynchronous HTTP GET request and automatically construct a Cheep object from the
// JSON object in the body of the response
if (arguments["read"].IsTrue)
{
    var cheeps = await client.GetFromJsonAsync<IEnumerable<Cheep>>("cheeps");
    UserInterface.printCheeps(cheeps);
    
}
else if (arguments["cheep"].IsTrue)
{
    await client.PostAsJsonAsync("cheep", makeCheep(arguments["<message>"].ToString()));
}
else
{
    Console.WriteLine("Invalid operation");
}

Cheep makeCheep(string message)
{
    var unixTimestamp = ((DateTimeOffset)DateTime.Now).ToUnixTimeSeconds();
    return new Cheep { Author = Environment.UserName, Message = message, Timestamp = unixTimestamp };
}