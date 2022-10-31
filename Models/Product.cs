using System.ComponentModel.DataAnnotations.Schema;

namespace PTLab2.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public int NewPrice { get; set; }
    }
}
