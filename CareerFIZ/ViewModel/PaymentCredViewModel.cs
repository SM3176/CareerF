using System.Xml.Linq;

namespace CareerFIZ.ViewModel
{
    public class PaymentCredViewModel
    {
        public string cardNumber {get;set; }
        public string cardHolder { get; set; }
        public DateTime expiryDate { get; set; }
        public int cvv {  get; set; }
    }
}
