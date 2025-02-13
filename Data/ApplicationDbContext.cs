using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SqlToMySql.Data.models;

namespace SqlToMySql.Data;

public class ApplicationDbContext : IdentityDbContext<
    AppUser, 
    AppRole, 
    int, 
    IdentityUserClaim<int>,
    AppUserRole,
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>, 
    IdentityUserToken<int>
    >
    
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<models.Class_Procedure> procedures { get; set; }
        public DbSet<Class_Patient> patients { get; set; }
       



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

