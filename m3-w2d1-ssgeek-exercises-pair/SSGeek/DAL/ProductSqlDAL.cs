using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SSGeek.Models;
using System.Data.SqlClient;

namespace SSGeek.DAL
{
    public class ProductSqlDAL : IProductDAL
    {
        string connectionString = @"Data Source=DESKTOP-58F8CH1\SQLEXPRESS;Initial Catalog=AlienDB;Integrated Security=True";
        string SQL_SelectProducts = @"select product_id, name, description, price, image_name FROM products";
        string SQL_GetProduct = @"SELECT * FROM products WHERE product_id = @productID";

        public Product GetProduct(int id)
        {
            Product p = new Product();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(SQL_GetProduct, conn);

                

                cmd.Parameters.AddWithValue("@productID", id);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    
                    p.ProductId = Convert.ToInt32(reader["product_id"]);
                    p.Name = Convert.ToString(reader["name"]);
                    p.Description = Convert.ToString(reader["description"]);
                    p.Price = Convert.ToDouble(reader["price"]);
                    p.ImageName = Convert.ToString(reader["image_name"]);

                }
            }
            return p;
        }
       
        public List<Product> GetProducts()
        {
            List<Product> productList = new List<Product>();

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(SQL_SelectProducts, conn);

                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Product p = new Product();
                        p.ProductId = Convert.ToInt32(reader["product_id"]);
                        p.Name = Convert.ToString(reader["name"]);
                        p.Description = Convert.ToString(reader["description"]);
                        p.Price = Convert.ToDouble(reader["price"]);
                        p.ImageName = Convert.ToString(reader["image_name"]);

                        productList.Add(p);
                    }
                }
            }
            catch (SqlException ex)
            {
                //Log and throw the exception
                throw;
            }

            return productList;
        }

		
    }
}