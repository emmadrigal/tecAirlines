namespace reservationMicroservice.Models
{

    /*
     * This class holds the information for a payment with a credit card
     */
    public class CardInfo
    {
        public string[] ReservationNumbers { get; set; }
        public long CardNumber { get; set; }
        public int SecurityCode { get; set; }
        public long Amount { get; set; }
    }
}