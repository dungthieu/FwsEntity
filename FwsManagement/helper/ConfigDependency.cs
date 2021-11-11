using Fws.DataAccess.Interface;
using Fws.DataAccess.Reponsitory;
using Fws.Service.Interface;
using Fws.Service.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FwsManagement.helper
{
    public static class ConfigDependency
    {
        public static IServiceCollection AddDependency(this IServiceCollection services)
        {
            // repository
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();

            // service
            services.AddScoped(typeof(IBaseService<>), typeof(BaseService<>));
            services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
