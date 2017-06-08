using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace promotionMicroservice.Models
{
	public class PromotionCreation
	{
		private string text; //information of the promotion
		private DateTime expirationDate;//expiration date of the promotion
		private string id; //identifier of the promotion

		public string Text { get; set; } // getter and setter of text
		public DateTime ExpirationTime { get; set; } // getter and setter of Expiration
		public string Id { get; set; } // getter and setter of ID
	}
}