using DAL.Models;
using DAL.Repository;

namespace BL.Services;

public class UserService(RepositoryManager repository)
{
    public async Task<User> GetUserByIdAsync(int id)
    {
        return await repository.GetEntityByIdAsync<User>(id);
    }
    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await repository.GetRangeAsync<User>();
    }
    public async Task<bool> CreateUserAsync(User entity)
    {
        var id = await repository.InsertEntityAsync(entity);
        return id > 0;
    }
    public async Task<bool> UpdateUserAsync(User entity)
    {
        return await repository.UpdateEntityAsync(entity);
    }
    public async Task<bool> DeleteUserAsync(int id)
    {
        return await repository.DeleteEntityAsync<User>(id);
    }
}
