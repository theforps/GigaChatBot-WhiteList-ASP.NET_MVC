using WhiteList_Web.Models;

namespace WhiteList_Web.Data;

public interface IWebRepository
{
    Task<User> getUserByUsername(string username);
    Task<List<User>> getAllUsers();
    Task changeBan(int id);
    Task<List<History>> getHistory(int id);

}
