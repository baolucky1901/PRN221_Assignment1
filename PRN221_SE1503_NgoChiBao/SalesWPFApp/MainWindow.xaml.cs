using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _configuration; 
        public MainWindow(IMemberRepository memberRepository, IProductRepository productRepository,
                            IOrderRepository orderRepository, IConfiguration configuration,
                            IAuthRepository authRepository)
        {
            InitializeComponent();
            _memberRepository = memberRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _authRepository = authRepository;
            _configuration = configuration;
        }

        private void Button_Member(object sender, RoutedEventArgs e)
        {
            this.Close();
            WindowMembers windowMembers = new WindowMembers(_configuration, _memberRepository, _productRepository, _orderRepository, _authRepository);
            windowMembers.ShowDialog();
        }

        private void Button_Order(object sender, RoutedEventArgs e)
        {
            this.Close();
            WindowOrders windowOrders = new WindowOrders(_configuration, _memberRepository, _productRepository, _orderRepository, _authRepository);
            windowOrders.ShowDialog();
        }

        private void Button_Product(object sender, RoutedEventArgs e)
        {
            this.Close();
            WindowProducts windowProducts = new WindowProducts(_configuration, _memberRepository, _productRepository, _orderRepository, _authRepository);
            windowProducts.ShowDialog();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            WindowLogin windowLogin = new WindowLogin(_configuration, _memberRepository, _productRepository, _orderRepository, _authRepository);
            windowLogin.ShowDialog();
        }

        private void btnHomePage_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
