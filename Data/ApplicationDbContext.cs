using Microsoft.EntityFrameworkCore;
using SqlToMySql.Data.SqlEntities;

namespace SqlToMySql.Data;

public class ApplicationDbContext: DbContext
    {
 public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof (ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<procedure_info> Procedure_infos {get; set;}





    }