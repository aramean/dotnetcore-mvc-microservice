using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Orders.Data
{
    public static class OrderSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new OrderContext(serviceProvider.GetRequiredService<DbContextOptions<OrderContext>>());
            if (context.Orders.Any())
            {
                return; // DB has been seeded.
            }

            context.Orders.AddRange(
                new Order { OrderNumber = 1, OrderRegistrationNumber = 1, OrderStatus = 0 },
                new Order { OrderNumber = 2, OrderRegistrationNumber = 2, OrderStatus = 0 },
                new Order { OrderNumber = 3, OrderRegistrationNumber = 3, OrderStatus = 0 }
            );

            context.SaveChanges();
        }
    }
}