using Chirp.Core;
using Chirp.Infrastructure.ChirpRepositories;
using Chirp.Infrastructure.ChirpServices;


public interface IFollowService
{
    Task<List<FollowerDto>> GetFollowers(string followerUser);
    Task AddFollower(string followerUser, string followedUser);

    Task Unfollow(string followerUser, string followedUser);
    
    Task<List<FollowerDto>> GetsFollowed(string followedUser);

}

public class FollowService : IFollowService
{
    private readonly IFollowerRepository _followerRepository;

    public FollowService(IFollowerRepository followerRepository)
    {
        _followerRepository = followerRepository;
    }

    public async Task<List<FollowerDto>> GetFollowers(string followerUser)
    {
        return await _followerRepository.GetFollowers(followerUser);
    }

    public async Task AddFollower(string followerUser, string followedUser)
    {
        await _followerRepository.AddFollower(followerUser, followedUser);
    }

    public async Task Unfollow(string followerUser, string followedUser)
    {
        await _followerRepository.UnFollow(followerUser, followedUser);
    }
    
    public async Task<List<FollowerDto>> GetsFollowed(string followedUser)
    {
        return await _followerRepository.GetsFollowed(followedUser); 
    }
    
    
}