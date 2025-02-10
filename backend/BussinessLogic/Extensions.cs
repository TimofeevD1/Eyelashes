using BussinessLogic.Interfaces;
using BussinessLogic.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogic
{
    public static class Extensions
    {
        public static IServiceCollection AddBussinessLogic(this IServiceCollection services) 
        {
            services.AddScoped<IBookingCalendarService, BookingCalendarService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IServiceService, ServiceService>();
            services.AddScoped<IAboutMeService, AboutMeService>();
            services.AddScoped<IPromoService, PromoService>();
            return services;
        }
    }
}
