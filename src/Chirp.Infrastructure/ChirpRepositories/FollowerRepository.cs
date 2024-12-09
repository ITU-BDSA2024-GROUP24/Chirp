using Chirp.Core;
using Microsoft.EntityFrameworkCore;
    
namespace Chirp.Infrastructure.ChirpRepositories;




public interface IFollowerRepository { 
    public Task AddFollower(string followerUser, string followedUser);
    
    public Task<List<FollowerDTO>> GetFollowers(string followerUser);
    
    public Task<List<FollowerDTO>> GetsFollowed(string followedUser);
    
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
        var AlreadyAFollower = _context.Followers.FirstOrDefault(f =>
            f.FollowedBy == followerUser && f.FollowThem == followedUser);

        if (AlreadyAFollower != null)
        {
            return;
        }

        var newFollowing = new Following
        {
            FollowThem = followedUser,
            FollowedBy = followerUser
        };

        await _context.Followers.AddAsync(newFollowing);
        await _context.SaveChangesAsync();
    }

    public async Task<List<FollowerDTO>> GetFollowers(string followerUser)
    {
        var query = from follower in _context.Followers
            where follower.FollowThem == followerUser
            select new FollowerDTO
            {
                Followers = follower.FollowedBy
            };

        return await query.ToListAsync();
    }

    public async Task<List<FollowerDTO>> GetsFollowed(string followedUser)
    {
        var query = from follow in _context.Followers
            where follow.FollowedBy == followedUser
            select new FollowerDTO
            {
                Followers = follow.FollowThem
            };

        return await query.ToListAsync();
    }

    public async Task UnFollow(string followerUser, string followedUser)
    {
        var existingFollowing = _context.Followers.FirstOrDefault(f =>
            f.FollowedBy == followerUser && f.FollowThem == followedUser);

        if (existingFollowing != null)
        {
            _context.Followers.Remove(existingFollowing);
            await _context.SaveChangesAsync();
        }
    }
}


