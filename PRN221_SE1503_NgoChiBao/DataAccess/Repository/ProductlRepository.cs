using BusinessObject.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(Product product) => ProductDAO.Instance.Remove(product);
        public Product GetProductByID(int productId) => ProductDAO.Instance.GetProductByID(productId);
        public IEnumerable<Product> GetProducts() => ProductDAO.Instance.GetProductsList();
        public void InsertProduct(Product product) => ProductDAO.Instance.AddNew(product);
        public void UpdateProduct(Product product) => ProductDAO.Instance.Update(product);
    }
}
