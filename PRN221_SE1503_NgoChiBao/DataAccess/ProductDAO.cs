using BusinessObject.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO 
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }
        public IEnumerable<Product> GetProductsList()
        {
            List<Product> products;

            try
            {
                var fStoreDB = new FStoreDBContext();
                products = fStoreDB.Products.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            return products;
        }

        public Product GetProductByID(int productID)
        {
            Product product = null;
            try
            {
                var fStoreDB = new FStoreDBContext();
                product = fStoreDB.Products.SingleOrDefault(product => product.ProductId == productID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return product;
        }

        public void AddNew(Product product)
        {
            try
            {
                Product _product = GetProductByID(product.ProductId);
                if (_product == null)
                {
                    var fStoreDB = new FStoreDBContext();
                    fStoreDB.Products.Add(product);
                    fStoreDB.SaveChanges();
                }
                else
                {
                    throw new Exception("Product is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Product product)
        {
            try
            {
                Product p = GetProductByID(product.ProductId);
                if (p != null)
                {
                    var fStoreDB = new FStoreDBContext();
                    fStoreDB.Entry<Product>(product).State = EntityState.Modified;
                    fStoreDB.SaveChanges();
                }
                else
                {
                    throw new Exception("Product does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Remove(Product product)
        {
            try
            {
                Product _product = GetProductByID(product.ProductId);
                if (_product != null)
                {
                    var fStoreDB = new FStoreDBContext();
                    fStoreDB.Products.Remove(_product);
                    fStoreDB.SaveChanges();
                }
                else
                {
                    throw new Exception("Product does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
