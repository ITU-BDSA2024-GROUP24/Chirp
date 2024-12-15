using Chirp.Core;
using Microsoft.EntityFrameworkCore;
    
namespace Chirp.Infrastructure.ChirpRepositories;




public interface IFollowerRepository { 
    public Task AddFollower(string followerUser, string followedUser);
    
    public Task<List<FollowerDto>> GetFollowers(string followerUser);
    
    public Task<List<FollowerDto>> GetsFollowed(string followedUser);
    
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
        var alreadyAFollower = _context.Followers.FirstOrDefault(f =>
            f.FollowedBy == followerUser && f.FollowThem == followedUser);

        if (alreadyAFollower != null)
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

    public async Task<List<FollowerDto>> GetFollowers(string followerUser)
    {
        var query = from follower in _context.Followers
            where follower.FollowThem == followerUser
            select new FollowerDto
            {
                Followers = follower.FollowedBy
            };
        var list = await query.ToListAsync();
        return list;
    }
   
    public async Task<List<FollowerDto>> GetsFollowed(string followedUser)
    {
        var query = from follow in _context.Followers
            where follow.FollowedBy == followedUser
            select new FollowerDto
            {
                Followers = follow.FollowThem
            };
        var list = await query.ToListAsync();
        return list;
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


