using WhiteList_Web.Models;
using WhiteList_Web.Models.DTO;

namespace WhiteList_Web.Services;

public interface IWebService
{
    Task<int> logIn(DTOAccount account);
    Task<List<User>> getUsers();
    Task updateBanInfo(int id);
    Task<List<History>> getHistoryOfUser(int id);
}
