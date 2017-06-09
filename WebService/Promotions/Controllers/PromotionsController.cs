using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using promotionMicroservice.Models;
using System.Threading;

namespace promotionMicroservice.Controllers

{

	[RoutePrefix("promotion")]
	public class PromotionsController : ApiController
	{
		/// <summary>
		/// Access to the data layer
		/// </summary>
		PromotionAccessService dataLayer = new PromotionAccessService("promotions");

		public static int contador = 1;


		/// <summary>
		/// This post method is use to add a promotion to the data layer 
		/// </summary>
		/// <param name="newPromotion">is the new promotion to be added</param>
		/// <returns>The result of the Http action</returns>
		// POST: promotion/add
		[Route("add")]
		[HttpPost]
		public IHttpActionResult AddPromotion([FromBody]Models.PromotionCreation newPromotion)
		{
			PromotionAccessService dataLayer = new PromotionAccessService("promotions");
			int newID = dataLayer.getSize() + 1;
			dataLayer.AddPromotion(newID.ToString(), newPromotion.Text, newPromotion.ExpirationTime);
			return Ok();
		}
		/// <summary>
		/// It is used to update the text of a promotion depending on the promotion parameter id
		/// </summary>
		/// <param name="newPromotion"></param>
		/// <returns>returns a result of the Http action to confirm the  update</returns>
		// Put: promotion/update
		[Route("update")]
		[HttpPut]
		public IHttpActionResult UpdatePromotion([FromBody]Models.PromotionCreation newPromotion)
		{
			PromotionAccessService dataLayer = new PromotionAccessService("promotions");
			dataLayer.Update(newPromotion.Id, newPromotion.Text, newPromotion.ExpirationTime);
			return Ok();
		}
		/// <summary>
		/// It is used to get all the promotion available
		/// </summary>
		/// <returns>returns all the promotions</returns>
		// GET: promotion/all
		[Route("all")] 
		public IHttpActionResult GetPromotions()
		{
			PromotionAccessService dataLayer = new PromotionAccessService("promotions");	
			return Ok(dataLayer.SelectPromotions());
		}
		/// <summary>
		/// It is used to get a promotion depending on the promotion parameter id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>The promotion according to the id</returns>
		// GET: promotion/5
		[Route("{id}")]
		[HttpGet]
		public IHttpActionResult GetPromotion(string id)
		{
			return Ok(dataLayer.SelectPromotion(id));
		}
		

	}

}