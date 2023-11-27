using GigaChat_Bot.models;

namespace GigaChat_Bot.repositories.interfaces;

public interface IUserRepository
{
    Task<User> getUserByUsername(string? username);
    Task addUser(User user);
    Task<List<History>> getHistory(int userId);
    Task addHistory(History history);
    Task<User> getUserById(int id);
    Task clearHistory(string username);
    Task clearHistory(long chatId);
}