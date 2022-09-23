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
using System.Windows.Shapes;

namespace SalesWPFApp
{
    /// <summary>
    /// Interaction logic for WindowHistory.xaml
    /// </summary>
    public partial class WindowHistory : Window
    {
        private readonly IConfiguration _configuration;
        private readonly IMemberRepository _memberRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthRepository _authRepository;
        public WindowHistory(IConfiguration configuration, IMemberRepository memberRepository,
                            IProductRepository productRepository, IOrderRepository orderRepository,
                            IAuthRepository authRepository)
        {
            InitializeComponent();
            _configuration = configuration;
            _memberRepository = memberRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _authRepository = authRepository;
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindowMember mainWindowMember = new MainWindowMember(_configuration, _memberRepository, _productRepository, _orderRepository, _authRepository);
            mainWindowMember.ShowDialog();
        }
    }
}
