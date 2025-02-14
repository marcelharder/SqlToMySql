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
        public DbSet<Class_Patient> patients { get; set; }
        public DbSet<Class_Procedure> procedures { get; set; }
        public DbSet<Class_Valve> Valves { get; set; }
        public DbSet<Class_CABG> CABGS { get; set; }
        public DbSet<Class_CPB> CPBS { get; set; }
        public DbSet<Class_PostOp> PostOps { get; set; }
        public DbSet<Class_Ref_Phys> RefPhys { get; set; }
        public DbSet<Class_Aortic_Surgery> AorticSurgeries { get; set; }
        public DbSet<Class_minInv> MinInvs { get; set; }
      
     
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

