using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Core;

[PrimaryKey(nameof(FollowThem), nameof(FollowedBy))]
public class Following

{
    public required string FollowThem { get; set; }
    
    public required string FollowedBy { get; set; }
}
    
