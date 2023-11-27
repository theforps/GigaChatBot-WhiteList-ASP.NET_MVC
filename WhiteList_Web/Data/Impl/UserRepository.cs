using GigaChat_Bot.repositories;
using WhiteList_Web.Data.Interfaces;

namespace WhiteList_Web.Data.Impl;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext db) {
        
        _db = db;
    }






}
