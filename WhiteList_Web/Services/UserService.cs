using System.Diagnostics;
using WhiteList_Web.Data.Interfaces;
using WhiteList_Web.models;
using WhiteList_Web.Models;
using WhiteList_Web.Resources;
using WhiteList_Web.Services.Interfaces;

namespace WhiteList_Web.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) {
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

        public async Task<bool> logIn(DTOAccount account)
        {
            try
            {
                var user = await _userRepository.getUserByUsername(account.login);

                if(user != null && user.Password!.Equals(account.password))
                {
                    Consts.currentUser = user.Id;

                    return true;
                }

                return false;
            }
            catch(Exception ex) { 
                
                Debug.WriteLine(ex.Message);
                return false;
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
}
