using GigaChat_Bot.models;

namespace GigaChat_Bot.repositories.interfaces;

public interface IHistoryRepository
{
    Task<List<History>> getHistory(int userId);
    Task addHistory(History history);
    Task clearHistory(string username);
    Task clearHistory(long chatId);
}
