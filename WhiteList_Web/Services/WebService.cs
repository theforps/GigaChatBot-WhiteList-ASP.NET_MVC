using System.Diagnostics;
using WhiteList_Web.Data;
using WhiteList_Web.Models;
using WhiteList_Web.Models.DTO;

namespace WhiteList_Web.Services;

public class WebService : IWebService
{
    private readonly IWebRepository _userRepository;
    public WebService(IWebRepository userRepository) {
        _userRepository = userRepository;
    }

    public async Task<List<History>> getHistoryOfUser(int id)
    {
        try
        {
            var history = await _userRepository.getHistory(id);

            return history;
        }
        catch (Exception ex) 
        {
            Debug.WriteLine(ex.Message);

            return null;
        }
    }

    public async Task<List<User>> getUsers()
    {
        try
        {
            var user = await _userRepository.getAllUsers();

            if(user != null) {
                return user;
            }

            return null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);

            return null;
        }
    }

    public async Task<int> logIn(DTOAccount account)
    {
        try
        {
            var user = await _userRepository.getUserByUsername(account.login);

            if(user != null && user.Password!.Equals(account.password))
            {
                return user.Id;
            }

            return -1;
        }
        catch(Exception ex) { 
            
            Debug.WriteLine(ex.Message);
            return -1;
        }
    }

    public async Task updateBanInfo(int id)
    {
        try
        {
            await _userRepository.changeBan(id);
        }
        catch (Exception ex) 
        {
            Debug.WriteLine($"{ex.Message}");
        }
    }
}
