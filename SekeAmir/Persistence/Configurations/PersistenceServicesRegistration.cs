
using Application.Contracts.Repository;
using Application.Contracts.Shop;
using Application.Contracts.Users;
using Domain.Account;
using Domain.Account.Permission;
using Domain.Shop;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repository;
using Persistence.Repository.Shop;
using Persistence.Repository.Users;

namespace Persistence.Configurations;

public static class PersistenceServicesRegistration
{
	public static IServiceCollection ConfigurePersistenceServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<SekeAminContext>(options =>
		{
			options.UseSqlServer(configuration.GetConnectionString("Connections"));
		});

		#region IOC
		services.AddScoped<IUser, UserServices>();
        services.AddScoped<IRole, RoleServices>();
        services.AddScoped<IRolePermission, RolePermissionServices>();
        services.AddScoped<IPermisionList, PermissionListServices>();



        services.AddScoped<IMaster<User>, MasterServices<User>>();
        services.AddScoped<IMaster<Role>, MasterServices<Role>>();
        services.AddScoped<IMaster<PermissionList>, MasterServices<PermissionList>>();
        services.AddScoped<IMaster<RolePermission>, MasterServices<RolePermission>>();
        services.AddScoped<IMaster<UserRole>, MasterServices<UserRole>>();

        #endregion
        #region Shop
        services.AddScoped<ICategory, CategoryServices>();
        services.AddScoped<IProduct, ProductServices>();
        services.AddScoped<IProductPrice, ProductPriceServices>();


        services.AddScoped<IMaster<Category>, MasterServices<Category>>();
        services.AddScoped<IMaster<Product>, MasterServices<Product>>();
        services.AddScoped<IMaster<Product>, MasterServices<Product>>();
        services.AddScoped<IMaster<ProductPrice>, MasterServices<ProductPrice>>();
        #endregion

        return services;
	}
}