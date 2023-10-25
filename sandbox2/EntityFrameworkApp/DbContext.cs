using Microsoft.EntityFrameworkCore;

using NullableUnitGeneratorSample.Sandbox.EntityFrameworkApp;

namespace EntityFrameworkApp;


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
