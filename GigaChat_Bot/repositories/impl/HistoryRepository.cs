using GigaChat_Bot.models;
using GigaChat_Bot.repositories.interfaces;
using Microsoft.EntityFrameworkCore;

namespace GigaChat_Bot.repositories.impl;

public class HistoryRepository : IHistoryRepository
{
    private readonly ApplicationDbContext _db = new ();

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

    public async Task clearHistory(long chatId)
    {
        var history = await _db.history.Where(x => x.User.ChatId == chatId).ToListAsync();

        if (history != null)
        {
            _db.history.RemoveRange(history);
            await _db.SaveChangesAsync();
        }
    }
}
