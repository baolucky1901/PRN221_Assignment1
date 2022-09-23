using BusinessObject.DataAccess;
using DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for MainWindowMember.xaml
    /// </summary>
    public partial class MainWindowMember : Window
    {
        private readonly IConfiguration _configuration;
        private readonly IMemberRepository _memberRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthRepository _authRepository;
        public MainWindowMember(IConfiguration configuration, IMemberRepository memberRepository,
                            IProductRepository productRepository, IOrderRepository orderRepository,
                            IAuthRepository authRepository)
        {
            InitializeComponent();
            _configuration = configuration;
            _memberRepository = memberRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _authRepository = authRepository;
            LoadMember();
        }

        private Member GetMemberByUsername(string username)
        {
            Member member = null;
            try
            {
                var fStoreDBContext = new FStoreDBContext();
                member = fStoreDBContext.Members.FirstOrDefault(x => x.Email == username);
            }
            catch (Exception)
            {

                throw;
            }
            return member;
        }
        
        private Member GetMember()
        {
            var email = ((WindowLogin)Application.Current.MainWindow).txtUsername.Text;
            var member = GetMemberByUsername(email);
            return member;
        }

        public bool CantChange { get; set; }

        private Member LoadMember()
        {
            Member member = null;
            //txtMemberId.IsEnabled = !CantChange;
            try
            {
                var memberObject = GetMember();
                txtMemberId.Text = memberObject.MemberId.ToString();
                txtEmail.Text = memberObject.Email.ToString();
                txtCountry.Text = memberObject.Country.ToString();
                txtCompanyName.Text = memberObject.CompanyName.ToString();
                txtCity.Text = memberObject.City.ToString();
                txtPassword.Text = memberObject.Password.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        private Member GetMemberObject()
        {
            Member member = null;
            member = new Member
            {
                MemberId = int.Parse(txtMemberId.Text),
                Email = txtEmail.Text,
                Country = txtCountry.Text,
                CompanyName = txtCompanyName.Text,
                City = txtCity.Text,
                Password = txtPassword.Text
            };
            return member;
        }


        private void Save_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
                var member = GetMemberObject();
                MessageBoxResult messageResult = MessageBox.Show($" Do you really want to update this {member.MemberId}?",
                                                                    "Update Information", MessageBoxButton.YesNo);
                if (messageResult == MessageBoxResult.Yes)
                {
                    _memberRepository.UpdateMember(member);
                    MessageBox.Show("Successfully!", "Update Information");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update Information");
            }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        => Close();

        private void ButtonChangeAvatar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonHistory_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            WindowHistory windowHistory = new WindowHistory(_configuration, _memberRepository, _productRepository, _orderRepository, _authRepository);
            windowHistory.ShowDialog();
        }
    }
}
