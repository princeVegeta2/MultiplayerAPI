﻿using GameAPI.Models;
using System.Collections.Generic;
using System.Linq;

namespace GameAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly GameContext _context;



        public UserRepository(GameContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public void CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }

        public void DeleteUser(int userId)
        {
            var user = _context.Users.Find(userId);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
