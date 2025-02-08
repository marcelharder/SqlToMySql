using Microsoft.EntityFrameworkCore;
using SqlToMySql.Data.SqlEntities;

namespace SqlToMySql.Data;

public class ApplicationDbContext: DbContext
    {
 public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
                : base(options)
            { }

       public DbSet<procedure_info> Procedure_infos {get; set;}





    }