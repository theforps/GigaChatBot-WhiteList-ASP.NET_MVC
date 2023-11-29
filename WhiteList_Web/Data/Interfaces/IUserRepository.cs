using WhiteList_Web.models;

namespace WhiteList_Web.Data.Interfaces;

public interface IUserRepository
{
    Task<User> getUserByUsername(string username);
    Task<List<User>> getAllUsers();
    Task changeBan(int id);
    Task<List<History>> getHistory(int id);

}
