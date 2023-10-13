using GameAPI.Models;
using System.Collections.Generic;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers();
    User GetUser(int id);
    void CreateUser(User user);
    void UpdateUser(User user);
    void DeleteUser(int userId);
    
}
