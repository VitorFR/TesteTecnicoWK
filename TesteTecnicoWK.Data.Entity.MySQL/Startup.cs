using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TesteTecnicoWK.Data.Entity.MySQL.Contexto;

namespace TesteTecnicoWK.Data.Entity.MySQL
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void ConfigurationServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("Conexao");
            services.AddDbContext<MySqlDbContext>(options =>
                 options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));
        }
    }
}
