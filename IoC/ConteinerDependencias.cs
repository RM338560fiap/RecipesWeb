using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repositorio.Context;

namespace IoC
{
    public class ConteinerDependencias
    {
        public static void RegistroServicos(IServiceCollection services)
        {
            var connection = @"Server=(localdb)\mssqllocaldb;Database=RecipesWeb;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(connection));

        }
    }
}
