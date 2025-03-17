using Microsoft.EntityFrameworkCore;
using PatientAPI.Models;
using System.Diagnostics;
namespace PatientAPI.Data
{
    public class PatientDbContext(DbContextOptions<PatientDbContext> options) : DbContext(options)
    {
        public DbSet<Patient> Patients => Set<Patient>();
    }

}
