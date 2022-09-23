using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;
        public App()
        {
            ServiceCollection services = new ServiceCollection();
            ConfigurationServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        //dependency injection (DI) - m dung j thi phai dang ky de dung cai DI
        private void ConfigurationServices(ServiceCollection services)
        {
            services.AddSingleton(typeof(IMemberRepository), typeof(MemberRepository));
            services.AddSingleton<WindowMembers>();

            services.AddSingleton(typeof(IProductRepository), typeof(ProductRepository));
            services.AddSingleton<WindowProducts>();

            services.AddSingleton(typeof(IOrderRepository), typeof(OrderRepository));
            services.AddSingleton<WindowOrders>();

            services.AddSingleton(typeof(IAuthRepository), typeof(AuthRepository));

            services.AddSingleton<WindowLogin>();
            services.AddSingleton<MainWindow>();
            services.AddSingleton<WindowHistory>();


            services.AddSingleton(typeof(IConfiguration), AddConfiguration());
        }

        private void OnStartup(object sender, StartupEventArgs e)
        {
            var windowLogin = serviceProvider.GetService<WindowLogin>();
            windowLogin.Show();
        }

        private IConfiguration AddConfiguration()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("D:\\PRN221\\Assignment1\\PRN221_SE1503_NgoChiBao\\SalesWPFApp\\appsettings.json");

            return builder.Build();
        }
    }
}
