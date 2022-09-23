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
    /// Interaction logic for WindowProducts.xaml
    /// </summary>
    public partial class WindowProducts : Window
    {
        private readonly IConfiguration _configuration;
        private readonly IMemberRepository _memberRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IAuthRepository _authRepository;
        public WindowProducts(IConfiguration configuration, IMemberRepository memberRepository,
                            IProductRepository productRepository, IOrderRepository orderRepository,
                            IAuthRepository authRepository)
        {
            InitializeComponent();
            _configuration = configuration;
            _memberRepository = memberRepository;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _authRepository = authRepository;
            LoadProductList();
        }

        private Product GetProduct()
        {
            Product product = null;
            try
            {
                product = new Product
                {
                    ProductId = int.Parse(txtProductId.Text),
                    CategoryId = int.Parse(txtCategoryId.Text),
                    ProductName = txtProductName.Text,
                    Weight = txtWeight.Text,
                    UnitPrice = decimal.Parse(txtUnitPrice.Text),
                    UnitsInStock = int.Parse(txtUnitsInStock.Text)
                };
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        private void LoadProductList()
        {
            try
            {
                lvProducts.ItemsSource = _productRepository.GetProducts();
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
                Product product = GetProduct();
                MessageBoxResult messageResult = MessageBox.Show($" Do you really want to insert this {product.ProductId}?",
                                                                    "Insert product", MessageBoxButton.YesNo);


                Regex validDigit = new Regex("^[Z0-9]*$");
                Regex validCharacterDigit = new Regex("^[a-zA-Z0-9 ]*$");

                Match matchProductID = validDigit.Match(txtProductId.Text);
                Match matchCategoryID = validDigit.Match(txtCategoryId.Text);
                Match matchProductName = validCharacterDigit.Match(txtProductName.Text);
                Match matchWeight = validCharacterDigit.Match(txtWeight.Text);
                Match matchUnitPrice = validDigit.Match(txtUnitPrice.Text);
                Match matchUnitsInStock = validDigit.Match(txtUnitsInStock.Text);

                if (    txtProductId.Text.Trim().Length <= 0
                    ||  txtCategoryId.Text.Trim().Length <= 0
                    ||  txtProductName.Text.Trim().Length <= 0
                    ||  txtWeight.Text.Trim().Length <= 0
                    ||  txtUnitPrice.Text.Trim().Length <= 0
                    ||  txtUnitsInStock.Text.Trim().Length <= 0)
                {
                    throw new Exception("All Fields can not be empty! ");
                }
                else if (!matchProductID.Success)
                {
                    throw new Exception("ProductId can not have special characters! ");
                }
                else if (!matchCategoryID.Success)
                {
                    throw new Exception("CategoryId can not have special characters! ");
                }

                else if (txtProductName.Text.Trim().Length > 20 || !matchProductName.Success)
                {
                    throw new Exception("Product name must be less than 20 characters can not have special characters! ");
                }

                else if (txtWeight.Text.Trim().Length > 20 || !matchWeight.Success)
                {
                    throw new Exception("Weight must be less than 20 characters can not have special characters! ");
                }

                else if (!matchUnitPrice.Success)
                {
                    throw new Exception("Unit Price must be a number and larger than or equal 0! ");
                }

                else if (!matchUnitsInStock.Success)
                {
                    throw new Exception("Units in Stock must be a number and larger than or equal 0! ");
                }

                else
                {
                    _productRepository.InsertProduct(product);
                    LoadProductList();
                    MessageBox.Show("Successfully!", "Insert product");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Insert product");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = GetProduct();
                MessageBoxResult messageResult = MessageBox.Show($" Do you really want to update this {product.ProductId}?",
                                                                    "Update product", MessageBoxButton.YesNo);
                if (messageResult == MessageBoxResult.Yes)
                {
                    _productRepository.UpdateProduct(product);
                    LoadProductList();
                    MessageBox.Show("Successfully!", "Update product");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Update product");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product product = GetProduct();
                MessageBoxResult messageResult = MessageBox.Show($" Do you really want to delete this {product.ProductId}?",
                                                                    "Delete product", MessageBoxButton.YesNo);
                if (messageResult == MessageBoxResult.Yes)
                {
                    _productRepository.DeleteProduct(product);
                    LoadProductList();
                    MessageBox.Show("Successfully!", "Delete product");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete product");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(_memberRepository, _productRepository, _orderRepository, _configuration, _authRepository);
            mainWindow.ShowDialog();
        }

    }
}
