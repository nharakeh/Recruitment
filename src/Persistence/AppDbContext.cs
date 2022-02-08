using Microsoft.EntityFrameworkCore;

namespace Vuture.Persistence
{
    public class ContactDbContext : DbContext
    {
        private ILoggerFactory GetLoggerFactory()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddLogging(builder => builder
                .AddDebug()
                .AddFilter(DbLoggerCategory.Database.Command.Name, LogLevel.Debug));
            return serviceCollection.BuildServiceProvider()
                    .GetService<ILoggerFactory>();
        }
        public ContactDbContext(DbContextOptions<ContactDbContext> opt) : base(opt)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(GetLoggerFactory());
        }

        public DbSet<Contact> Contacts { get; set; } = null!;
    }
}