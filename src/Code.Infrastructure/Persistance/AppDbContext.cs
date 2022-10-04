namespace Code.Infrastructure.Persistance
{
    public class AppDbContext : ApiAuthorizationDbContext<AppUser>, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions) { }

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }
    }
}
