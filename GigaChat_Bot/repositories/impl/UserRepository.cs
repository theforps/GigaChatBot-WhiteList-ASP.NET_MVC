using Microsoft.EntityFrameworkCore;
using GigaChat_Bot.models;
using GigaChat_Bot.repositories.interfaces;

namespace GigaChat_Bot.repositories.impl;

public class UserRepository: IUserRepository
{
    private readonly ApplicationDbContext _db = new();

    public async Task<User> getUserByUsername(string? username)
    {
        var user = await _db.users.FirstOrDefaultAsync(x => x.Username.Equals(username));

        return user!;
    }

    public async Task addUser(User user)
    {
        await _db.users.AddAsync(user);
        await _db.SaveChangesAsync();
    }

    public async Task<User> getUserById(int id)
    {   
        var user = await _db.users.FirstOrDefaultAsync(x => x.Id == id);

        return user!;
    }
}