using FootballStats.ApplicationModule.Common.Interfaces.Repositories;
using FootballStats.Domain.Entities;

namespace FootballStats.Infrastructure.Persistence.Repositories;

public class SignUpRepository : ISignUpRepository
{
    private readonly IApplicationDbContext _context;

    public SignUpRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddUser(User user)
    {
        await _context.Users.AddAsync(user);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public bool UserExist(string email, string username)
    {
        return _context.Users
            .Any(user => user.Email == email
            || user.UserName == username);
    }
}