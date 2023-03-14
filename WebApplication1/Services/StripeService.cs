using CWDBreedingAPI.Entities;
using CWDBreedingAPI.Models.Non_EntityModels;
using CWDBreedingAPI.Repos;
using ReviewPlatformAPI.Entities;
using ReviewPlatformAPI.Models;
using ReviewPlatformAPI.Models.Non_EntityModels;
using ReviewPlatformAPI.Repos;
using Stripe;
using Stripe.Checkout;

namespace ReviewPlatformAPI.Services
{
    public class StripeService
    {
        private readonly IConfiguration _configuration;
        private readonly RanchRepo _ranchRepo;
        private readonly DeerRepo _deerRepo;
        private readonly PromoCodeRepository _promoCodeRepository;

        public StripeService(IConfiguration configuration, RanchRepo ranchRepo, DeerRepo deerRepo, PromoCodeRepository repository)
        {
            _configuration = configuration;
            _ranchRepo = ranchRepo;
            _deerRepo = deerRepo;
            _promoCodeRepository = repository;


        }

        //Create-Checkout-Session
        public StripeSessionModel CreateSubCheckoutSession(List<DeerSubsciptionModel> deerToPayFor)
        {
            if (deerToPayFor[0].RanchId != null && deerToPayFor[0].RanchId != null)
            {
                try
                {
                    StripeConfiguration.ApiKey = _configuration["StripeAPIKey"];

                    Guid ranchId = Guid.Parse(deerToPayFor[0].RanchId);

                    Ranch ranch = _ranchRepo.GetByNoTrackingId(ranchId);

                    var domain = _configuration["CurrentHost"];

                    List<SessionLineItemOptions> lineItems = new List<SessionLineItemOptions>();

                    foreach (DeerSubsciptionModel deer in deerToPayFor)
                    {
                        SessionLineItemOptions lineItem = new SessionLineItemOptions();
                        lineItem.Quantity = 1;

                        Dictionary<string, string> metaData = new Dictionary<string, string>();
                        metaData.Add("DeerId", deer.DeerId);

                        ProductCreateOptions productOptions = new ProductCreateOptions
                        {
                            Name = deer.DeerName,
                            Metadata = metaData,
                        };

                        productOptions.DefaultPriceData = new ProductDefaultPriceDataOptions();
                        
                        productOptions.DefaultPriceData.Currency = "usd";

                        long price = long.Parse(_configuration["DeerCost"]);
                        productOptions.DefaultPriceData.UnitAmountDecimal = price * 100;
                        
                        productOptions.DefaultPriceData.Recurring = new ProductDefaultPriceDataRecurringOptions();
                        productOptions.DefaultPriceData.Recurring.Interval = "year";

                        var productOptionsService = new ProductService();
                        Product x = productOptionsService.Create(productOptions);

                        lineItem.Price = x.DefaultPriceId;

                        lineItems.Add(lineItem);

                    }

                    //Create Stripe Session that takes in list of deer to pay for as one subscription
                    var options = new SessionCreateOptions
                    {
                         LineItems = new List<SessionLineItemOptions>
                    {
                    },
                        Mode = "subscription",
                        SuccessUrl = domain + "/success.html",
                        CancelUrl = domain + "/cancel.html",
                    };

                    options.LineItems = lineItems;
                    options.CustomerEmail = ranch.Email;
                    
                    var service = new SessionService();
                    Session session = service.Create(options);

                    StripeSessionModel stripeSession = new StripeSessionModel();
                    stripeSession.StripeURl = session.Url;

                    return stripeSession;
                } 
                catch
                {
                    throw new Exception("Payment Failed");
                }
              
            }
            else
            {
                throw new Exception("Empty Data Set");
            }
        }

        public object? InvoicePaymentFailedHandler(Event stripeEvent)
        {
            //Mark Deer as unpaid at end of current subscription, alert user
            //Mark Deer as Paid, alert user
            var invoice = stripeEvent.Data.Object as Invoice;

            if (invoice == null)
            {
                throw new Exception("Invoice Not Found");
            }
            else
            {
                foreach (var lineItem in invoice.Lines.Data)
                {
                    var deerId = lineItem.Metadata["deerId"];
                    Deer? deer = _deerRepo.LoadByPrimaryKey(Guid.Parse(deerId));

                    if (deer == null)
                    {
                        throw new Exception("Deer Not Found");
                    }
                    else
                    {
                        deer.IsPaid = false;
                        _deerRepo.Update(deer);
                    }
                }
            }

            return "Failed to pay invoice";
        }

        public PromoCode? GetPromoCode(string? promoCode)
        {
            return _promoCodeRepository.GetPromoCode(promoCode);
        }

        internal object? CanceledSubscriptionHandler(Event stripeEvent)
        {
            throw new NotImplementedException();
        }

        public object? InvoicePaidHandler(Event stripeEvent)
        {
            //Mark Deer as Paid, alert user
            var invoice = stripeEvent.Data.Object as Invoice;

            if (invoice == null)
            {
                throw new Exception("Invoice Not Found");
            } 
            else
            {
                foreach (var lineItem in invoice.Lines.Data)
                {
                    var deerId = lineItem.Metadata["deerId"];
                    Deer? deer = _deerRepo.LoadByPrimaryKey(Guid.Parse(deerId));

                    if (deer == null)
                    {
                        throw new Exception("Deer Not Found");
                    }
                    else
                    {
                        deer.IsPaid = true;
                        _deerRepo.Update(deer);
                    }
                }
            }

            return "Success";
        }

        public object? CheckoutCompletedHandler(Event stripeEvent)
        {
            //Mark Deer as paid
            Session? checkoutSession = stripeEvent.Data.Object as Session;

            if (checkoutSession == null)
            {
                throw new Exception("Session Not Found");
            }
            else
            {
                Ranch ranch = _ranchRepo.GetRanchByEmail(checkoutSession.CustomerEmail);
                ranch.StripeId = checkoutSession.CustomerId;
                _ranchRepo.Update(ranch);

                var service = new SessionService();
                
                var options = new SessionListLineItemsOptions
                {
                    Limit = 100,
                    Expand = new List<string> { "data.price.product" }
                };
                
                StripeList<LineItem> lineItems = service.ListLineItems(checkoutSession.Id, options);

                //mark each deer as paid
                foreach (LineItem item in lineItems.Data)
                {

                    string? deerId = item.Price.Product.Metadata["DeerId"];

                    if (deerId != null)
                    {
                        Guid deerGuid = Guid.Parse(deerId);
                        Deer? deer = _deerRepo.LoadByPrimaryKey(deerGuid);
                        if (deer == null)
                        {
                            throw new Exception("Deer Not Found");
                        }
                        else
                        {
                            deer.IsPaid = true;
                            _deerRepo.Update(deer);
                        }
                    }
                }

            }

            return "Success";
        }

        internal object? InvoiceCreatedHandler(Event stripeEvent)
        {
            throw new NotImplementedException();
        }
    }
}
