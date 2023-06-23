using Microsoft.EntityFrameworkCore;

using NullableUnitGenerator;
using UGO = NullableUnitGenerator.UnitGenerateOption;

namespace EntityFrameworkApp;

[UnitOf(typeof(int), UGO.ParseMethod | UGO.EntityFrameworkValueConverter)]
public readonly partial struct UserId { }

public class User
{
    public UserId UserId { get; set; }
}

public class SampleDbContext : DbContext
{
    public SampleDbContext() { } 
    public SampleDbContext(DbContextOptions<SampleDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; } = default!;

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<UserId>().HaveConversion<UserId.UserIdEntityFrameworkValueConverter>();
    }
}
