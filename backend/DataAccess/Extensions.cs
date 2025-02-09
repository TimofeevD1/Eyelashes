using DataAccess.Interfaces;
using DataAccess.Repositories;
using DataAccess.Repositorys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
    public static class Extensions
    {

        public static IServiceCollection AddDataAccess(this IServiceCollection services, string connectionString)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IBookingCalendarRepository, BookingCalendarRepository>();
            services.AddScoped<IAboutMeRepository, AboutMeRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddDbContext<AppContext>(x => 
            {
                x.UseNpgsql(connectionString);
            });
            return services;
        }
    }
}
