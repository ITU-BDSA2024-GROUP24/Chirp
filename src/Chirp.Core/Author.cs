using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Chirp.Core;

public class Author : IdentityUser
{ 
    
    [Required] 
    public required string DisplayName { get; set; }
    
    [Required]
    public required ICollection<Cheep> Cheeps;
    
}