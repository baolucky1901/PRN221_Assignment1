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
    /// Interaction logic for WindowOrders.xaml
    /// </summary>
    public partial class WindowOrders : Window
    {
        private readonly IConfiguration _configuration;
        private readonly IMemberRepository _memberRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthRepository _authRepository;
        public WindowOrders(IConfiguration configuration, IMemberRepository memberRepository,
                            IProductRepository productRepository, IOrderRepository orderRepository,
                            IAuthRepository authRepository)
        {
            InitializeComponent();
            _configuration = configuration;
            _memberRepository = memberRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _authRepository = authRepository;
            LoadOrderList();
        }

        private Order GetOder()
        {
            Order order = null;
            try
            {
                order = new Order
                {
                    OrderId = int.Parse(txtOrderId.Text),
                    MemberId = int.Parse(txtMemberId.Text),
                    OrderDate = DateTime.Parse(txtOrderDate.Text),
                    RequiredDate = DateTime.Parse(txtRequiredDate.Text),
                    ShippedDate = DateTime.Parse(txtShippedDate.Text),
                    Freight = decimal.Parse(txtFreight.Text)
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return order;
        }

        private void LoadOrderList()
        {
            try
            {
                lvOrders.ItemsSource = _orderRepository.GetOrders();
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
                Order order = GetOder();
                MessageBoxResult messageResult = MessageBox.Show($" Do you really want to insert this {order.OrderId}?",
                                                                    "Insert order", MessageBoxButton.YesNo);

                Regex validDitgit = new Regex("^[Z0-9]*$");
                Regex validCharacterDigit = new Regex("^[a-zA-Z0-9 ]*$");

                Match matchOrderID = validCharacterDigit.Match(txtOrderId.Text);
                Match matchMemberID = validCharacterDigit.Match(txtMemberId.Text);
                Match matchFreight = validDitgit.Match(txtFreight.Text);

                if (    txtOrderId.Text.Trim().Length <= 0
                    ||  txtMemberId.Text.Trim().Length <= 0
                    ||  txtOrderDate.Text.Trim().Length <= 0
                    ||  txtRequiredDate.Text.Trim().Length <= 0
                    ||  txtShippedDate.Text.Trim().Length <= 0
                    ||  txtFreight.Text.Trim().Length <= 0)
                {
                    throw new Exception("All Fields can not be empty! ");
                }
                else if (!matchOrderID.Success)
                {
                    throw new Exception("OrderId can not have special characters! ");
                }
                else if (!matchMemberID.Success)
                {
                    throw new Exception("MemberId can not have special characters! ");
                }

                else if (!matchFreight.Success)
                {
                    throw new Exception("Order date must be a number and larger than 0! ");
                }

                else
                {
                    _orderRepository.InsertOrder(order);
                    LoadOrderList();
                    MessageBox.Show("Successfully!", "Insert order");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert order");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order order = GetOder();
                MessageBoxResult messageResult = MessageBox.Show($" Do you really want to update this {order.OrderId}?",
                                                                    "Update order", MessageBoxButton.YesNo);

                Regex validDigit = new Regex("^[Z0-9]*$");

                Match matchOrderID = validDigit.Match(txtOrderId.Text);
                Match matchMemberID = validDigit.Match(txtMemberId.Text);
                Match matchFreight = validDigit.Match(txtFreight.Text);

                if (txtOrderId.Text.Trim().Length <= 0
                    || txtMemberId.Text.Trim().Length <= 0
                    || txtOrderDate.Text.Trim().Length <= 0
                    || txtRequiredDate.Text.Trim().Length <= 0
                    || txtShippedDate.Text.Trim().Length <= 0
                    || txtFreight.Text.Trim().Length <= 0)
                {
                    throw new Exception("All Fields can not be empty! ");
                }
                else if (!matchOrderID.Success)
                {
                    throw new Exception("OrderId must be less than 15 and can not have special characters! ");
                }
                else if (!matchMemberID.Success)
                {
                    throw new Exception("MemberId must be less than 15 can not have special characters! ");
                }

                else if (!matchFreight.Success)
                {
                    throw new Exception("Order date must be a number and larger than or equal 0! ");
                }

                else
                {
                    _orderRepository.UpdateOrder(order);
                    LoadOrderList();
                    MessageBox.Show("Successfully!", "Update order");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update order");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Order order = GetOder();
                MessageBoxResult messageResult = MessageBox.Show($" Do you really want to delete this {order.OrderId}?",
                                                                    "Delete order", MessageBoxButton.YesNo);
                if (messageResult == MessageBoxResult.Yes)
                {
                    _orderRepository.DeleteOrder(order);
                    LoadOrderList();
                    MessageBox.Show("Successfully!", "Delete order");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete order");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_memberRepository, _productRepository, _orderRepository, _configuration, _authRepository);
            mainWindow.ShowDialog();
        }

    }
}

