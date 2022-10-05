using Code.Infrastructure.Persistance.Interceptors;

namespace Code.Infrastructure.Persistance
{
    public class AppDbContext : ApiAuthorizationDbContext<AppUser>, IApplicationDbContext
    {
        private readonly AuditableEntitySaveChangesInterceptor _auditableEntitySaveChanges;

        public AppDbContext(AuditableEntitySaveChangesInterceptor auditableEntitySaveChanges, DbContextOptions<AppDbContext> options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
            this._auditableEntitySaveChanges = auditableEntitySaveChanges;
        }

        public DbSet<Category> Categories => Set<Category>();

        public DbSet<Product> Products => Set<Product>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(builder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(_auditableEntitySaveChanges);
            base.OnConfiguring(optionsBuilder);
        }
    }
}
