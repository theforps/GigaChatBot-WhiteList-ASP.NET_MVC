using GigaChat_Bot.models;

namespace GigaChat_Bot.repositories.interfaces;

public interface IUserRepository
{
    Task<User> getUserByUsername(string? username);
    Task addUser(User user);
    Task<User> getUserById(int id);
}