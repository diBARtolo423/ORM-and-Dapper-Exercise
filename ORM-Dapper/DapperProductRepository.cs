using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM products;");
        }
        public void CreateProduct(string name, double price, int categoryID)
        {
            _connection.Execute("INSERT INTO PRODUCTS (Name, Price, CategoryID) VALUES (@productName, @price, @catID);",
             new { productName = name, price = price, catID = categoryID });
        }

        public void GetProduct(string name)
        {
           _connection.QuerySingle<Product>("SELECT * FROM products WHERE Name = @name;",
                new { name = name }); 
        }

        public void UpdateProduct(int productID, string name)
        {
            _connection.Execute("UPDATE products SET Name = @name WHERE ProductID = @productID;",
             new { name = name, productID = productID });
        }

        public void DeleteProduct(int productID)
        {
            _connection.Execute("DELETE FROM products, reviews WHERE ProductID = @productID;",
                new { productID = productID });
            _connection.Execute("DELETE FROM sales, reviews WHERE ProductID = @productID;",
                new { productID = productID });
            _connection.Execute("DELETE FROM reviews, reviews WHERE ProductID = @productID;",
                new { productID = productID });
        }

       
    }
}
