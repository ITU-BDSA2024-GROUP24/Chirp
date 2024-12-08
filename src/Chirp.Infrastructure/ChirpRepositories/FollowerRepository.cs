using Chirp.Core;
using Microsoft.EntityFrameworkCore;
    
namespace Chirp.Infrastructure.ChirpRepositories;




public interface IFollowerRepository { 
    public Task AddFollower(string followerUser, string followedUser);
    
    public Task<List<FollowerDTO>> GetFollowers(string followerUser);
    
    public Task<List<FollowerDTO>> Getsfollowed(string followedUser);
    
    public Task UnFollow(string followerUser, string followedUser);
    
    
}


public class FollowerRepository : IFollowerRepository
{
    private readonly ChirpDBContext _context;

    public FollowerRepository(ChirpDBContext context)
    {
        _context = context;
    }

    public async Task AddFollower(string followerUser, string followedUser)
    {
        var following = _context.Followers.FirstOrDefault(following => following.followedBy == followedUser && following.followThem == followerUser);
       if (following != null)
       {
           return;
       }

       var newFollowing = new Following()
       {
           FollowThem = followedUser,
           FollowedBy = followerUser
       };
       
        await _context.Followers.AddAsync(newFollowing);
        await _context.SaveChangesAsync();
      
    }


    public async Task<List<FollowerDTO>> GetFollowers(string followerUser)
    {
       
    }

    public async Task<List<FollowerDTO>> Getsfollowed(string followedUser)
    {
        
    }

    public async Task UnFollow(string followerUser, string followedUser)
    {
        var following = _context.Followers.FirstOrDefault(following => following.followedBy == followerUser && following.followThem == followedUser);

       
    }
    

}