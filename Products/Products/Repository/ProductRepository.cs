using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Products.Models;
using System.Data;

namespace Products.Repository
{
    public class ProductRepository
    {
        private SqlConnection con;

        private void connection()
        {
            string constr = ConfigurationManager.ConnectionStrings["ProductDatabase"].ToString();
            con = new SqlConnection(constr);
        }

        public List<ProductModel> GetAllProducts()
        {
            connection();
            List<ProductModel> ProdList = new List<ProductModel>();
            SqlCommand com = new SqlCommand("Select P.ProductId, P.ProductName, C.CategoryId, C.CategoryName from SampleDB.dbo.Product P join SampleDB.dbo.Category C on C.CategoryId = P.CategoryId order by ProductId ;", con);
            com.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataTable dt = new DataTable();

            con.Open();
            da.Fill(dt);
            con.Close();
            foreach (DataRow dr in dt.Rows)
            {

                ProdList.Add(

                    new ProductModel
                    {
                        ProductId = Convert.ToInt32(dr["ProductId"]),
                        ProductName = Convert.ToString(dr["ProductName"]),
                        CategoryId = Convert.ToInt32(dr["CategoryId"]),
                        CategoryName = Convert.ToString(dr["CategoryName"])
                    }
                    );
            }

            return ProdList;
        }

        internal bool AddProd(ProductModel pd)
        {
            connection();
            SqlCommand com = new SqlCommand("Insert into SampleDB.dbo.Product (ProductId, ProductName, CategoryId) Values (@ProductID, @ProductName, @CategoryId)", con);
            com.CommandType = CommandType.Text;
            com.Parameters.AddWithValue("@ProductID", pd.ProductId);
            com.Parameters.AddWithValue("@ProductName", pd.ProductName);
            com.Parameters.AddWithValue("@CategoryId", pd.CategoryId);
            
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }    
        }


        internal bool UpdateProduct(ProductModel pd)
        {
            connection();
            SqlCommand com = new SqlCommand("Update Product Set ProductId =@ProductID, ProductName=@ProductName,CategoryId= @CategoryId where ProductId = @ProductID", con);

            com.CommandType = CommandType.Text;
            com.Parameters.AddWithValue("@ProductID", pd.ProductId);
            com.Parameters.AddWithValue("@ProductName", pd.ProductName);
            com.Parameters.AddWithValue("@CategoryId", pd.CategoryId);
            
            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DeleteProduct(int Id)
        {

            connection();
            SqlCommand com = new SqlCommand("Delete from Product where  ProductId =@ProductID", con);

            com.CommandType = CommandType.Text;
            com.Parameters.AddWithValue("@ProductID", Id);

            con.Open();
            int i = com.ExecuteNonQuery();
            con.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {

                return false;
            }
        }    
    }
}