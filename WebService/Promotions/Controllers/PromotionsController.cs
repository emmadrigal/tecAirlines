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

	[RoutePrefix("api/promotion")]
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
		// POST: api/Promotions/add
		[Route("add")]
		[HttpPost]
		public IHttpActionResult AddPromotion([FromBody]Models.PromotionCreation newPromotion)
		{
			PromotionAccessService dataLayer = new PromotionAccessService("promotions");
			dataLayer.AddPromotion(newPromotion.Id, newPromotion.Text, newPromotion.ExpirationTime);
			return Ok();
		}
		/// <summary>
		/// It is used to update the text of a promotion depending on the promotion parameter id
		/// </summary>
		/// <param name="newPromotion"></param>
		/// <returns>returns a result of the Http action to confirm the  update</returns>
		// POST: api/Promotions/update
		[Route("update")]
		[HttpPut]
		public IHttpActionResult UpdatePromotion([FromBody]Models.PromotionCreation newPromotion)
		{
			PromotionAccessService dataLayer = new PromotionAccessService("promotions");
			dataLayer.Update(newPromotion.Id, newPromotion.Text);
			return Ok();
		}
		/// <summary>
		/// It is used to get all the promotion available
		/// </summary>
		/// <returns>returns all the promotions</returns>
		// GET: api/Promotions/all
		[Route("all")] 
		public IHttpActionResult GetPromotions()
		{
			int seconds = 3;
			var tid1 = new Thread(() => {
				
				while (true)
				{
					
					for(contador = 1; contador < 5; contador++)
					{
						Thread.Sleep(seconds * 1000);

						System.Diagnostics.Debug.WriteLine(contador);

					}
					
					
				}
			});
			tid1.Start();
			tid1.Join(1000);
			PromotionAccessService dataLayer = new PromotionAccessService("promotions");
			
			
			
			return Ok(dataLayer.SelectPromotion(contador.ToString()));
		}
		/// <summary>
		/// It is used to get a promotion depending on the promotion parameter id
		/// </summary>
		/// <param name="id"></param>
		/// <returns>The promotion according to the id</returns>
		// GET: api/Promotions/5
		[Route("{id:int}")]
		[HttpGet]
		public IHttpActionResult GetPromotion(string id)
		{
			return Ok(dataLayer.SelectPromotion(id));
		}
		

	}

}