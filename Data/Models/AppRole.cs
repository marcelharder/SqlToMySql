namespace SqlToMySql.Data.models;

public class AppRole: IdentityRole<int> {
        public ICollection<AppUserRole>? UserRoles { get; set; }
    }