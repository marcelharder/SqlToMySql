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
        
        public DbSet<Class_Employee> Employees { get; set; }
        public DbSet<Class_Patient> Patients { get; set; }
        public DbSet<Class_Procedure> Procedures { get; set; }
        public DbSet<Class_Valve> Valves { get; set; }
        public DbSet<Class_CABG> CABGS { get; set; }
        public DbSet<Class_CPB> CPBS { get; set; }
        public DbSet<Class_PostOp> PostOps { get; set; }
        public DbSet<Class_Ref_Phys> RefPhys { get; set; }
        public DbSet<Class_Preview_Operative_report> Previews { get; set; }
        public DbSet<Class_Aortic_Surgery> AorticSurgeries { get; set; }
        public DbSet<Class_minInv> MinInvs { get; set; }
        public DbSet<ClassTableVlad> Vlads { get; set; }
        public DbSet<Class_LTX> LTXs { get; set; }
        public DbSet<Class_User_Online> Online_users {get; set;}

              


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }

