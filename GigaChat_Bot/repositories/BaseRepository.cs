using GigaChat_Bot.models;
using Microsoft.EntityFrameworkCore;

namespace GigaChat_Bot.repositories;

public class BaseRepository
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

    public async Task<List<History>> getHistory(int userId)
    {
        return await _db.history.Where(x => x.User.Id == userId).ToListAsync();
    }

    public async Task addHistory(History history)
    {
        await _db.history.AddAsync(history);
        await _db.SaveChangesAsync();
    }

    public async Task clearHistory(string username)
    {
        var history = _db.history.Where(x => x.User.Username.Equals(username)).ToArray();

        if (history.Length > 0)
        {
            _db.history.RemoveRange(history);
            await _db.SaveChangesAsync();
        }
    }

    public async Task clearHistory(int userId)
    {
        var history = await _db.history.Where(x => x.User.Id == userId).ToListAsync();

        if (history != null && history.Count > 0)
        {
            _db.history.RemoveRange(history);
            await _db.SaveChangesAsync();
        }
    }
}
