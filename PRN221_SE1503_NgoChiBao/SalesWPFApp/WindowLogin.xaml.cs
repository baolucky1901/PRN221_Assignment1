using BusinessObject.DataAccess;
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
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        private readonly IConfiguration _configuration;
        private readonly IMemberRepository _memberRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthRepository _authRepository;
        public WindowLogin(IConfiguration configuration, IMemberRepository memberRepository, 
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

        private Member GetMember(string username, string password)
        {
            Member member = null;
            try
            {
                member = _authRepository.checkLogin(username, password);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // zo file appsettingjson de no lay section AdminAccount roi lay thong tin gan bao DefaultAccount
            var defaultAccount = _configuration.GetSection("AdminAccount").Get<DefaultAccount>();
            string email = defaultAccount.Email;
            string password = defaultAccount.Password;
            string role = defaultAccount.Role;

            string inputEmail = txtUsername.Text;
            string inputPassword = txtPasword.Password.ToString();

            var memberAccount = GetMember(inputEmail, inputPassword);

            if (memberAccount != null)
            {
                this.Hide();
                MainWindowMember mainWindowMember = new MainWindowMember(_configuration, _memberRepository, _productRepository, _orderRepository, _authRepository);
                mainWindowMember.ShowDialog();
            }
            else
            {

                if (!inputEmail.Equals(email) && !inputPassword.Equals(password))
                    MessageBox.Show("This user is not exist!", "Notificaiton");
                else
                {
                    this.Hide();
                    MainWindow mainWindow = new MainWindow(_memberRepository, _productRepository, _orderRepository, _configuration, _authRepository);
                    mainWindow.ShowDialog();
                }
            }
        }
    }
}
