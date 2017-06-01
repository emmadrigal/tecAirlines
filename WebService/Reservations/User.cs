using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace reservationMicroservice.Models
{
    public class User
    {
        private string name;
        private string surname1;
        private string surname2;
        private DateTime birthDate;
        private string phoneNumber;
        private long userID;
        private string email;
        private int cardNumber;
        private string userName;
        private string password;
        private string rol;
        private string carnet;
        private string university;
        private long frequentFlyerMiles;

        public string Name { get => name; set => name = value; }
        public string Surname1 { get => surname1; set => surname1 = value; }
        public string Surname2 { get => surname2; set => surname2 = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public string PhoneNumber { get => phoneNumber; set => phoneNumber = value; }
        public long UserID { get => userID; set => userID = value; }
        public string Email { get => email; set => email = value; }
        public int CardNumber { get => cardNumber; set => cardNumber = value; }
        public string UserName { get => userName; set => userName = value; }
        public string Password { get => password; set => password = value; }
        public string Rol { get => rol; set => rol = value; }
        public string Carnet { get => carnet; set => carnet = value; }
        public string University { get => university; set => university = value; }
        public long FrequentFlyerMiles { get => frequentFlyerMiles; set => frequentFlyerMiles = value; }
    }
}