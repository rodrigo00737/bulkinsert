using Lab.BulkInsert.Dados.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Lab.BulkInsert.Dados.Repository
{
    public class ExchangeContext : DbContext
    {
        protected readonly IConfiguration Configuration;
        private string connection;

        public ExchangeContext(IConfiguration configuration, string connection)
        {
            Configuration = configuration;
            this.connection = connection;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {            
            options.UseNpgsql(Configuration.GetConnectionString(this.connection));
        }

        public DbSet<Exchange> Exchanges{ get; set; }        
        
    }
}
