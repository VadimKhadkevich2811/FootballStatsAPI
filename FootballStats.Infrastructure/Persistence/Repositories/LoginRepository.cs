using FootballStats.ApplicationModule.Common.Interfaces;
using FootballStats.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FootballStats.Infrastructure.Persistence.Repositories;

public class LoginRepository : ILoginRepository
{
    private readonly IApplicationDbContext _context;

    public LoginRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetUserByEmailOrUsername(string loginId)
    {
        return await _context.Users
            .Where(user => user.UserName == loginId ||
                user.Email == loginId).FirstOrDefaultAsync();
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public async Task UpdateUserToken(User user, string token)
    {
        if (token != null)
        {
            user.Token = token;
            user.TokenEnd = DateTime.Now.AddSeconds(86400);
            await SaveChangesAsync();
        }
    }
}