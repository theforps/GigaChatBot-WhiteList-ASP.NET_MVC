using WhiteList_Web.models;
using WhiteList_Web.Models;

namespace WhiteList_Web.Services.Interfaces;

public interface IUserService
{
    Task<bool> logIn(DTOAccount account);
    Task<List<User>> getUsers();
    Task updateBanInfo(int id);
    Task<List<History>> getHistoryOfUser(int id);
}
