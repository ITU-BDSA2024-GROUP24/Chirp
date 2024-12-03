using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
namespace Chirp.Core;

public class Author : IdentityUser
{ 
    public required ICollection<Cheep> Cheeps;
    
}