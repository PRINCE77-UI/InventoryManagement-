using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using InventoryManagement.Models;

namespace InventoryManagement.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = @"Data Source=prince\sqlexpress;Initial Catalog=TestDB;Integrated Security=True";


        // GET: Product
        public ActionResult Index()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Products";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product();

                    product.Id = (int)reader["Id"];
                    product.Name = reader["Name"].ToString();
                    product.Category = reader["Category"].ToString();
                    product.Price = (decimal)reader["Price"];
                    product.Quantity = (int)reader["Quantity"];

                    products.Add(product);
                }
            }

            return View(products);
        }


        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"INSERT INTO Products
                                    (Name, Category, Price, Quantity)
                                    VALUES
                                    (@Name, @Category, @Price, @Quantity)";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Category", product.Category);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Quantity", product.Quantity);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }

            return View(product);
        }


        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = new Product();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Products WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    product.Id = (int)reader["Id"];
                    product.Name = reader["Name"].ToString();
                    product.Category = reader["Category"].ToString();
                    product.Price = (decimal)reader["Price"];
                    product.Quantity = (int)reader["Quantity"];
                }
            }

            return View(product);
        }


        // POST: Product/Edit
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                using (SqlConnection con = new SqlConnection(connectionString))
                {
                    string query = @"UPDATE Products
                                    SET Name=@Name,
                                        Category=@Category,
                                        Price=@Price,
                                        Quantity=@Quantity
                                    WHERE Id=@Id";

                    SqlCommand cmd = new SqlCommand(query, con);

                    cmd.Parameters.AddWithValue("@Id", product.Id);
                    cmd.Parameters.AddWithValue("@Name", product.Name);
                    cmd.Parameters.AddWithValue("@Category", product.Category);
                    cmd.Parameters.AddWithValue("@Price", product.Price);
                    cmd.Parameters.AddWithValue("@Quantity", product.Quantity);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }

            return View(product);
        }


        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string query = "DELETE FROM Products WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@Id", id);

                con.Open();

                cmd.ExecuteNonQuery();
            }

            return RedirectToAction("Index");
        }
    }
}