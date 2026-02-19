using Microsoft.EntityFrameworkCore;
using Notepad.Api.Models;
using System.Collections.Generic;
using Auth.Api.Models;

namespace Notepad.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

        
    }
}
