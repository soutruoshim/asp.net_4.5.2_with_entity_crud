using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MVCWOEF.Models;

namespace MVCWOEF.Controllers
{
    public class ProductController : Controller
    {
        string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\User\source\repos\MVCWOEF\MVCWOEF\App_Data\Database1.mdf;Integrated Security=True";
        
        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtblProduct = new DataTable();
            using(SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT * FROM PRODUCT", sqlCon);
                sqlDa.Fill(dtblProduct);
            }
            return View(dtblProduct);
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View(new Product());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(Product product)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();

                string query = "INSERT INTO Product VALUES (@ProductName, @Price, @Count)";

                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                sqlCmd.Parameters.AddWithValue("@Price", product.Price);
                sqlCmd.Parameters.AddWithValue("@Count", product.Count);
                sqlCmd.ExecuteNonQuery();
               
            }
            return RedirectToAction("Index");
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            Product product = new Product();
            DataTable dtblProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();

                string query = "SELECT * FROM Product Where ProductID = @ProductID";
               
                SqlDataAdapter sqlDa = new SqlDataAdapter(query, sqlCon);
                sqlDa.SelectCommand.Parameters.AddWithValue("@ProductID", id);
                sqlDa.Fill(dtblProduct);
            }
            if(dtblProduct.Rows.Count == 1)
            {
                product.ProductID = Convert.ToInt32(dtblProduct.Rows[0][0].ToString());
                product.ProductName = dtblProduct.Rows[0][1].ToString();
                product.Price = Convert.ToDecimal(dtblProduct.Rows[0][2].ToString());
                product.Count = Convert.ToInt32(dtblProduct.Rows[0][3].ToString());
                return View(product);
            }
            else
            {
               return RedirectToAction("Index");
            }
          
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();

                string query = "UPDATE Product SET ProductName = @ProductName, Price = @Price, Count = @Count Where ProductID = @ProductID";

                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                sqlCmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                sqlCmd.Parameters.AddWithValue("@Price", product.Price);
                sqlCmd.Parameters.AddWithValue("@Count", product.Count);
                sqlCmd.ExecuteNonQuery();

            }
            return RedirectToAction("Index");
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionString))
            {
                sqlCon.Open();

                string query = "DELETE FROM Product Where ProductID = @ProductID";

                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProductID", id);
         
                sqlCmd.ExecuteNonQuery();

            }
            return RedirectToAction("Index");
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
