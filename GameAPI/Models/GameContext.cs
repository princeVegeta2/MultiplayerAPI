﻿using Microsoft.EntityFrameworkCore;

namespace GameAPI.Models
{
    public class GameContext : DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}
