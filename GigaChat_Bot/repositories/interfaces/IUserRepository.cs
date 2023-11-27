using YandexGPT_bot.models;

namespace YandexGPT_bot.repositories.interfaces;

public interface IUserRepository
{
    Task<User> getUserByUsername(string? username);
    Task addUser(User user);
    Task<List<History>> getHistory(int userId);
    Task addHistory(History history);
    Task<User> getUserById(int id);
    Task clearHistory(string username);
}