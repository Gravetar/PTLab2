using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PTLab2.Controllers;
using PTLab2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTest
{
    internal class DependencyInjection
    {
        public static ServiceCollection InitilizeServices()
        {
            var services = new ServiceCollection();
            var options = new DbContextOptionsBuilder<ShopContext>().UseInMemoryDatabase("ShopDB").Options;
            services.AddScoped(_ => new ShopContext(options));
            return services;
        }

    }
}
