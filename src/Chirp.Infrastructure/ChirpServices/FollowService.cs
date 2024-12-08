using Chirp.Core;
using Chirp.Infrastructure.ChirpRepositories;

namespace Chirp.Infrastructure.ChirpServices;

public interface IFollowService
{
    public Task<List<FollowerDTO>> GetFollowers(string followerUser);
    
    public Task AddFollower(string followerUser, string followedUser);
    
    public Task Unfollow(string followerUser, string followedUser);
    
}

public class FollowService : IFollowService
{
    
}
