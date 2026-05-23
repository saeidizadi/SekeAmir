
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Configurations;

public static class PersistenceServicesRegistration
{
	public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<SekeAminContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("CatalogueConnection"));
		});

		#region IOC
		//services.AddScoped<IUnitOfWork, UnitOfWork>();
	
        #endregion

        return services;
	}
}