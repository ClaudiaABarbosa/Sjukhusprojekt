using JournalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace JournalAPI.Data
{
    public class JournalDbContext(DbContextOptions<JournalDbContext> options) : DbContext(options)
    {
        public DbSet<Journal> Journaler => Set<Journal>();
    }
  
}
