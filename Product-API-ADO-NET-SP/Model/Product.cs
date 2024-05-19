using System.ComponentModel.DataAnnotations;

namespace Product_API_ADO_NET_SP.Model
{
    public class Product
    {
        public int ActionId { get; set; } // store procedure er CRUD operation konta korbo seta select korar jnno.

        public int Id { get; set; }

        [Required, StringLength(50)]
        public string ProductName { get; set; }

        [Required, StringLength(30)]
        public string ProductCode { get; set; }

        [Required]
        public decimal SalePrice { get; set; }
        public string Brand { get; set; }
        public int ProductWarranty { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
