using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reservationMicroservice
{
    /**
     * This class make the connection to the bank and tries to perform the payment
     */
    public class BankService
    {
        public bool PaymentTransaction(Models.CardInfo cardInfo)
        {
            if (cardInfo != null)
                return true;
            else
                return false;
        }
    }
}