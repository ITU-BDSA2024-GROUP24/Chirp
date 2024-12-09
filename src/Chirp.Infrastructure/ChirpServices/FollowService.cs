using Chirp.Core;
using Chirp.Infrastructure.ChirpRepositories;



public interface IFollowService
{
    
}

public class FollowService : IFollowService
{
    private readonly IFollowerRepository _followerRepository;

    public FollowService(IFollowerRepository followerRepository)
    {
        _followerRepository = followerRepository;
    }

    public async Task<List<FollowerDTO>> GetFollowers(string followerUser)
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
}