using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Core;

[PrimaryKey(nameof(FollowThem), nameof(FollowedBy))]
public class Following

{
    [MaxLength(160)]
    public string? FollowThem { get; set; }
    
    [MaxLength(160)]
    public string? FollowedBy { get; set; }
}
    
