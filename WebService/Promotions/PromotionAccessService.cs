using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace promotionMicroservice
{
	public class PromotionAccessService : DatabaseAccess
	{

		public PromotionAccessService(string filename) : base(filename) {
			
		}
		/// <summary>
		/// This method gets all the promotions in the xml file
		/// </summary>
		/// <returns>The complete list of promotions</returns>
		public List<Models.Promotion> SelectPromotions()
		{
			XDocument document = XDocument.Load(Filename);
			XElement parent = document.Root;
			List<Models.Promotion> promotions = new List<Models.Promotion>();
			DateTime localDate = DateTime.Now;
			foreach (XElement element in parent.Elements())
			{
				int expiration = (DateTime.Compare(DateTime.Parse(element.Attribute("expirationTime").Value), localDate));
				if (expiration < 0)
				{
					Models.Promotion promotionToAdd = new Models.Promotion()
					{
						Id = element.Attribute("id").Value,
						ExpirationTime = DateTime.Parse(element.Attribute("expirationTime").Value),
						Text = element.Attribute("text").Value

					};
					promotions.Add(promotionToAdd);

				}
				else
				{

				}
				
			}
			return promotions;
		}




		/// <summary>
		/// This method gets acertain  promotion in the xml file denpending if the id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>A  promotion from the xml</returns>
		public Models.Promotion SelectPromotion(string id)
		{
			List<string> attributes = new List<string>
			{
				"id"
			};
			List<string> keys = new List<string>
			{
				id

			};
			List<XElement> promotionElement = GetElement(attributes, keys);
			Models.Promotion selectedPromotion = new Models.Promotion();
			selectedPromotion.Id = promotionElement[0].Attribute("id").Value;
			selectedPromotion.ExpirationTime = DateTime.Parse(promotionElement[0].Attribute("expirationTime").Value);
			selectedPromotion.Text = promotionElement[0].Attribute("text").Value;
			return selectedPromotion;

		}

		/// <summary>
		/// Add a new promotion to the xml with the parameters of the user
		/// </summary>
		/// <param name="id"></param>
		/// <param name="text"></param>
		/// <param name="expirationTime"></param>
		/// <returns>returns true when the add action is done</returns>
		public bool AddPromotion(String id,String text, DateTime expirationTime) {
			List<string> attributes = new List<string>
			{
				"id",
				"text",
				"expirationTime"
			};

			List<string> values = new List<string>
			{
				id,
				text,
				expirationTime.ToString()
			};
			
			AddElement("promotion", attributes, values);

			return true;
		}

		/// <summary>
		/// Update the text of a promotion
		/// </summary>
		/// <param name="id"></param>
		/// <param name="newText"></param>
		/// <returns>a boolean value if the change in the file is done</returns>
		public bool Update(string id, string newText)
		{
			List<string> attributes = new List<string>
			{
				"id"
			};
			List<string> keys = new List<string>
			{
				id
			};
			List<string> attributesToChange = new List<string>
			{
				"text"
			};
			List<string> changes = new List<string>
			{
				newText
			};

			return UpdateElement(attributes, keys, attributesToChange, changes);
		}

	}
}
