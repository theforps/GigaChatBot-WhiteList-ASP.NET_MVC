using GigaChat_Bot.repositories;
using Microsoft.EntityFrameworkCore;
using WhiteList_Web.Data.Interfaces;
using WhiteList_Web.models;

namespace WhiteList_Web.Data.Impl;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;
    public UserRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task changeBan(int id)
    {
        var user = await _db.users.FirstOrDefaultAsync(x => x.Id == id);

        if(user.Ban)
            user.Ban = false;
        else
            user.Ban = true;

        _db.users.Update(user);
        await _db.SaveChangesAsync();
    }

    public async Task<List<User>> getAllUsers()
    {
        var users = await _db.users.ToListAsync();

        return users;
    }

    public async Task<List<History>> getHistory(int id)
    {
        var history = await _db.history.Where(x => x.User.Id == id).ToListAsync();

        return history;
    }

    public async Task<User> getUserByUsername(string username)
    {
        var user = await _db.users.FirstOrDefaultAsync(x => x.Username == username);

        return user;
    }
}
