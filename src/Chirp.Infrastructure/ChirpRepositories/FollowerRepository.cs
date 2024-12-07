using Chirp.Core;

namespace Chirp.Infrastructure.ChirpRepositories;




public interface IFollowerRepository { 
    public Task AddFollower(string followerUser, string followedUser);
    
    public Task GetFollowers(string followerUser);
    
    public Task Getsfollowed(string followUser);
    
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
       /* var follow = _context.Followers.FirstOrDefault(follow => follow.Follower == followerUser && follow.Followed == followedUser);
        }
        /*var newFollow = new Follow(){
            Follower = followerUser,
            Followed = followedUser
        };
        await _context.Followers.AddAsync(newFollow);
        await _context.SaveChangesAsync();*/
      
    }


    public async Task GetFollowers(string followerUser)
    {
        
    }

    public async Task Getsfollowed(string followUser)
    {
    }

    public async Task UnFollow(string followerUser, string followedUser)
    {
        
    }



}