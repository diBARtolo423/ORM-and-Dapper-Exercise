using System;
using System.Data;
using System.IO;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace ORM_Dapper
{
    public class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            string connString = config.GetConnectionString("DefaultConnection");

            IDbConnection conn = new MySqlConnection(connString);

            //----------------------------------------------------------
            
            var repo = new DapperDepartmentRepository(conn);

            Console.WriteLine("Please enter a new department name");

            var newDepartment = Console.ReadLine();

            repo.InsertDepartment(newDepartment);

            var departments = repo.GetAllDepartments();
            foreach (var dept in departments)
            {
                Console.WriteLine(dept.Name);
            }

            //-----------------------------------------------------------

            var productRepo = new DapperProductRepository(conn);

            Console.WriteLine("Please enter a new product name");
            var newProduct = Console.ReadLine();
            Console.WriteLine("Please enter a new price");
            var newPrice = double.Parse(Console.ReadLine());
            Console.WriteLine("Please enter a new category ID");
            var newCategoryID = int.Parse(Console.ReadLine());

            productRepo.CreateProduct(newProduct, newPrice, newCategoryID);

            var products = productRepo.GetAllProducts();
            foreach (var prod in products)
            {
                Console.WriteLine($"{prod.ProductID} {prod.Name} {prod.Price}");                
            }


            Console.WriteLine("Which product ID do you want to update");
            var updateProductID = int.Parse(Console.ReadLine());
            Console.WriteLine("What is the new product name you want");
            var updateProductName = (Console.ReadLine());
            productRepo.UpdateProduct(updateProductID, updateProductName);

            products = productRepo.GetAllProducts();
            foreach (var prod in products)
            {
                Console.WriteLine($"{prod.ProductID} {prod.Name} {prod.Price}");
            }

            Console.WriteLine("Which product do you want to delete");
            var deleteProductID = int.Parse(Console.ReadLine());
            productRepo.DeleteProduct(deleteProductID);

            products = productRepo.GetAllProducts();
            foreach (var prod in products)
            {
                Console.WriteLine($"{prod.ProductID} {prod.Name} {prod.Price}");
            }



        }
    }
}
