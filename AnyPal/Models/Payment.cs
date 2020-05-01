using System;
namespace AnyPal.Models
{
    public class Payment
    {
        public Payment()
        {
            
        }

        public string Email { get; set; }
        public decimal Amount { get; set; }
        public string ItemName { get; set; }
        public string ItemNumber { get; set; }
        public string Name { get; set; }

    }
}
