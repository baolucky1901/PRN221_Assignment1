using BusinessObject.DataAccess;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for WindowMembers.xaml
    /// </summary>
    public partial class WindowMembers : Window
    {
        private readonly IConfiguration _configuration;
        private readonly IMemberRepository _memberRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthRepository _authRepository;
        public WindowMembers(IConfiguration configuration, IMemberRepository memberRepository,
                            IProductRepository productRepository, IOrderRepository orderRepository,
                            IAuthRepository authRepository)
        {
            InitializeComponent();
            _configuration = configuration;
            _memberRepository = memberRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _authRepository = authRepository;
            LoadMemberList();
        }

        private Member GetMember()
        {
            Member member = null;
            try
            {
                member = new Member
                {
                    MemberId = int.Parse(txtMemberId.Text),
                    Email = txtEmail.Text,
                    CompanyName = txtCompanyName.Text,
                    City = txtCity.Text,
                    Country = txtCountry.Text,
                    Password = txtPassword.Text
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return member;
        }

        private void LoadMemberList()
        {
            try
            {
                lvMembers.ItemsSource = _memberRepository.GetMembers();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void btnInsert_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member member = GetMember();
                MessageBoxResult messageResult = MessageBox.Show($" Do you really want to insert this {member.MemberId}?",
                                                                    "Insert member", MessageBoxButton.YesNo);
                
                Regex validEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Regex validCharacterDigit = new Regex("^[a-zA-Z0-9 ]*$");
                Regex validCharacter = new Regex("^[a-zA]*$");

                Match matchEmail = validEmail.Match(txtEmail.Text);
                Match matchID = validCharacterDigit.Match(txtMemberId.Text);
                Match matchCity = validCharacter.Match(txtCity.Text);
                Match matchCountry = validCharacter.Match(txtCountry.Text);
                

                if (messageResult == MessageBoxResult.Yes)
                {
                    if (txtMemberId.Text.Trim().Length <= 0
                    || txtEmail.Text.Trim().Length <= 0
                    || txtCompanyName.Text.Trim().Length <= 0
                    || txtCity.Text.Trim().Length <= 0
                    || txtCountry.Text.Trim().Length <= 0)
                    {
                        throw new Exception("All Fields can not be empty! ");
                    }

                    else if (!matchID.Success)
                    {
                        throw new Exception("MemberId must be a number and larger than 0! ");
                    }

                    else if (!matchEmail.Success)
                    {
                        throw new Exception("Email is invalid format");
                    }

                    else if (txtCompanyName.Text.Trim().Length > 50 || txtCompanyName.Text.Trim().Length < 6)
                    {
                        throw new Exception("Company name must be between 6 to 50 characters");
                    }

                    else if (txtCity.Text.Trim().Length > 50 || txtCity.Text.Trim().Length < 6
                                    || !matchCity.Success) 
                    {
                        throw new Exception("City must be between 6 to 50 characters and can not have special characters");
                    }
                    else if (txtCountry.Text.Trim().Length > 50 || txtCountry.Text.Trim().Length < 6
                                    || !matchCountry.Success)
                    {
                        throw new Exception("Country must be between 6 to 50 characters and can not have special characters");
                    }
                    else if (txtPassword.Text.Trim().Length > 50 || txtPassword.Text.Trim().Length < 6)
                    {
                        throw new Exception("Passwword must be between 6 to 50 characters");
                    }
                    else
                    {
                        _memberRepository.InsertMember(member);
                        LoadMemberList();
                        MessageBox.Show("Successfully!", "Insert member");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert member");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member member = GetMember();
                MessageBoxResult messageResult = MessageBox.Show($" Do you really want to update this {member.MemberId}?",
                                                                    "Update member", MessageBoxButton.YesNo);
                Regex validEmail = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Regex validDigit = new Regex("^[Z0-9]*$");
                Regex validCharacter = new Regex("^[a-zA]*$");

                Match matchEmail = validEmail.Match(txtEmail.Text);
                Match matchID = validDigit.Match(txtMemberId.Text);
                Match matchCity = validCharacter.Match(txtCity.Text);
                Match matchCountry = validCharacter.Match(txtCountry.Text);


                if (messageResult == MessageBoxResult.Yes)
                {

                    if (txtMemberId.Text.Trim().Length <= 0
                    || txtEmail.Text.Trim().Length <= 0
                    || txtCompanyName.Text.Trim().Length <= 0
                    || txtCity.Text.Trim().Length <= 0
                    || txtCountry.Text.Trim().Length <= 0)
                    {
                        throw new Exception("All Fields can not be empty! ");
                    }

                    else if (!matchID.Success)
                    {
                        throw new Exception("MemberId can not have special characters! ");
                    }

                    else if (!matchEmail.Success)
                    {
                        throw new Exception("Email is invalid format");
                    }

                    else if (txtCompanyName.Text.Trim().Length > 50 || txtCompanyName.Text.Trim().Length < 6)
                    {
                        throw new Exception("Company name must be between 6 to 50 characters");
                    }

                    else if (txtCity.Text.Trim().Length > 50 || txtCity.Text.Trim().Length < 6
                                    || !matchCity.Success)
                    {
                        throw new Exception("City must be between 6 to 50 characters and can not have special characters");
                    }

                    else if (txtCountry.Text.Trim().Length > 50 || txtCountry.Text.Trim().Length < 6
                                    || !matchCountry.Success)
                    {
                        throw new Exception("Country must be between 6 to 50 characters and can not have special characters");
                    }

                    else if (txtPassword.Text.Trim().Length > 50 || txtPassword.Text.Trim().Length < 6)
                    {
                        throw new Exception("Password must be between 6 to 50 characters");
                    }


                    else
                    {
                        _memberRepository.UpdateMember(member);
                        LoadMemberList();
                        MessageBox.Show("Successfully!", "Update member");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update member");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Member member = GetMember();
                MessageBoxResult messageResult = MessageBox.Show($" Do you really want to delete this {member.MemberId}?",
                                                                    "Delete member", MessageBoxButton.YesNo);
                if (messageResult == MessageBoxResult.Yes)
                {
                    _memberRepository.DeleteMember(member);
                    LoadMemberList();
                    MessageBox.Show("Successfully!", "Delete member");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete member");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow(_memberRepository, _productRepository, _orderRepository, _configuration, _authRepository);
            mainWindow.ShowDialog();
        }
        

    }
}
