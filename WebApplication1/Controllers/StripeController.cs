using System.Collections.Generic;
using CWDBreedingAPI.Models.Non_EntityModels;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReviewPlatformAPI.Services;
using Stripe;


namespace ReviewPlatformAPI.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class StripeController : Controller
    {
        private readonly StripeService _stripeService;
        private readonly IConfiguration _configuration;

        public StripeController(StripeService stripeService, IConfiguration configuration)
        {
            _stripeService = stripeService;
            _configuration = configuration;
        }

        //[HttpPut]
        //[Route("cancelSub")]
        //public ActionResult CancelSub()
        //{
        //    try
        //    {
        //        string id = User.Identity?.Name ?? "";
        //        if(!string.IsNullOrEmpty(id))
        //        {
        //            return Ok(_stripeService.CancelStripeSubAtEndOfBillingCycleByProviderId(id));
        //        } 
        //        else
        //        {
        //            throw new Exception("No User");
        //        }
        //    }
        //    catch
        //    {
        //        return BadRequest();
        //    }
        //}

        [HttpPost]
        [Route("create-checkout-session")]
        public ActionResult Create(List<DeerSubsciptionModel> deerToPayFor)
        {
            try
            {
                return Ok(_stripeService.CreateSubCheckoutSession(deerToPayFor));
            }
            catch
            {
                return BadRequest();
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("webhook")]
        public async Task<IActionResult> Webhook()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            Event stripeEvent;
            try
            {
                var webhookSecret = _configuration["WebhookKey"];
                stripeEvent = EventUtility.ConstructEvent(
            json,
            Request.Headers["Stripe-Signature"],
            webhookSecret
        );
                Console.WriteLine($"Webhook notification with type: {stripeEvent.Type} found for {stripeEvent.Id}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Something failed {e}");
                return BadRequest();
            }

            switch (stripeEvent.Type)
            {
                case "checkout.session.completed":         
                    try
                    {
                        return Ok(_stripeService.CheckoutCompletedHandler(stripeEvent));
                    }
                    catch
                    {
                        return BadRequest("Handler Failed");
                    }
                case "invoice.paid":
                    try
                    {
                        return Ok(_stripeService.InvoicePaidHandler(stripeEvent));
                    }
                    catch
                    {
                        return BadRequest("Handler Failed");
                    }
                case "invoice.payment_failed":
                    try
                    {
                        return Ok(_stripeService.InvoicePaymentFailedHandler(stripeEvent));
                    }
                    catch
                    {
                        return BadRequest("Handler Failed");
                    }
                default: return Ok("No Webhook Handler");
            }


        }
    }
}

