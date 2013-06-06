using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TestProject.Models
{
    public class PaymentModel
    {
        public CardInfo Card { get; set; }

        public double Price { get; set; }
        public int OrderId { get; set; }
        public double FinalPrice { get; set; }
    }
}