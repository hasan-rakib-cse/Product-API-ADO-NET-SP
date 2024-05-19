using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_API_ADO_NET_SP.Model;
using System.Data.SqlClient;
using System.Data;

namespace Product_API_ADO_NET_SP.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // action 1 = Create
        // action 2 = Read
        // action 3 = Update
        // action 4 = Delete

        [Route("ProductSP")]
        [HttpPost]
        public async Task<IActionResult> ProductSP(Product obj)
        {
            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
            SqlCommand cmd;
            SqlDataAdapter da;
            DataTable dt = new DataTable();
            try
            {
                cmd = new SqlCommand("SP_CRUD_Products", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@action", obj.ActionId);
                cmd.Parameters.AddWithValue("@Id", obj.Id);
                cmd.Parameters.AddWithValue("@ProductName", obj.ProductName);
                cmd.Parameters.AddWithValue("@ProductCode", obj.ProductCode);
                cmd.Parameters.AddWithValue("@SalePrice", obj.SalePrice);
                cmd.Parameters.AddWithValue("@Brand", obj.Brand);
                cmd.Parameters.AddWithValue("@ProductWarranty", obj.ProductWarranty);
                cmd.Parameters.AddWithValue("@CreatedDate", obj.CreatedDate);
                da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                // Read Operation
                if(obj.ActionId == 2)
                {
                    List<Product> products = new List<Product>();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Product product = new Product();
                        product.Id = Convert.ToInt32(dt.Rows[i]["Id"]);
                        product.ProductName = dt.Rows[i]["ProductName"].ToString();
                        product.ProductCode = dt.Rows[i]["ProductCode"].ToString();
                        product.SalePrice = Convert.ToDecimal(dt.Rows[i]["SalePrice"]);
                        product.Brand = dt.Rows[i]["Brand"].ToString();
                        product.ProductWarranty = Convert.ToInt32(dt.Rows[i]["ProductWarranty"]);
                        product.CreatedDate = Convert.ToDateTime(dt.Rows[i]["CreatedDate"]);
                        products.Add(product);
                    }
                    return Ok(products);
                }
                else
                {
                    return Ok(obj);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
