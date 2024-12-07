using Chirp.Core;

namespace Chirp.Infrastructure.ChirpRepositories;




public interface IFollowerRepository { 
    public Task AddFollower(string followerUser, string followedUser);
    
    public Task GetFollowers(string followerUser);
    
    public Task Getsfollowed(string followUser);
    
    public Task RemoveFollower(string followerUser, string followedUser);
    
    
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
      
    }


    public Task GetFollowers(string followerUser)
    {
        
    }

    public Task Getsfollowed(string followUser)
    {
    }

    public Task RemoveFollower(string followerUser, string followedUser)
    {
        
    }



}