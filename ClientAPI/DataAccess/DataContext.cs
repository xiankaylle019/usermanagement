using System.Linq;
using ClientAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ClientAPI.DataAccess
{
    public class DataContext : IdentityDbContext<ApplicationUser>
    {
        public DataContext(DbContextOptions<DataContext> options) :base(options){}
      
        public DbSet<Person> Person { get; set; }
        public DbSet<PersonContact> PersonContact { get; set; }

    }
}