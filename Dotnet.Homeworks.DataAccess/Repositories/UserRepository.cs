using Dotnet.Homeworks.Data.DatabaseContext;
using Dotnet.Homeworks.Domain.Abstractions.Repositories;
using Dotnet.Homeworks.Domain.Entities;

namespace Dotnet.Homeworks.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;
    
    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Task<IQueryable<User>> GetUsersAsync(CancellationToken cancellationToken)
    {
        return Task.FromResult(_dbContext.Users.AsQueryable());
    }

    public Task<User?> GetUserByGuidAsync(Guid guid, CancellationToken cancellationToken)
    {
        return Task.FromResult(_dbContext.Users.FirstOrDefault(u => u.Id == guid));
    }

    public Task DeleteUserByGuidAsync(Guid guid, CancellationToken cancellationToken)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == guid);
        if (user is null)
            return Task.FromException(new ArgumentException($"User with id {guid} not found"));
        _dbContext.Users.Remove(user);
        return Task.CompletedTask;
    }

    public Task UpdateUserAsync(User user, CancellationToken cancellationToken)
    {
        var userDb = _dbContext.Users.FirstOrDefault(u => user.Id == u.Id);
        userDb!.Name = user.Name;
        return Task.FromResult(_dbContext.Update(userDb));    
    }

    public async Task<Guid> InsertUserAsync(User user, CancellationToken cancellationToken)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
        return user.Id; //TODO check what id should i return: from Entity or from db
    }
}