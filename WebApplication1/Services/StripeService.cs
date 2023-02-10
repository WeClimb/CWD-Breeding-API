using CWDBreedingAPI.Models.Non_EntityModels;
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

        public StripeService(IConfiguration configuration, RanchRepo ranchRepo)
        {
            _configuration = configuration;
            _ranchRepo = ranchRepo;
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

                        ProductCreateOptions productOptions = new ProductCreateOptions
                        {
                            Name = deer.DeerName,
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



        //public bool CancelStripeSubAtEndOfBillingCycleByProviderId(string id)
        //{
        //    string? subId = "";

        //    if (!string.IsNullOrEmpty(id))
        //    {
        //        ServiceProvider provider = _serviceProviderRepo.GetByNoTrackingId(Guid.Parse(id));
        //        if (provider.SubData != null)
        //        {
        //            subId = provider.SubData.StripeSubId;
        //        }

        //        if (!string.IsNullOrEmpty(subId))
        //        {
        //            CancelStripeSubAtEndOfBillingCycle(subId);
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        //public object? CheckoutCompletedHandler(Event stripeEvent)
        //{
        //    Session? checkoutSession = stripeEvent.Data.Object as Session;

        //    if(checkoutSession == null)
        //    {
        //        throw new Exception("Session Not Found");
        //    } 
        //    else
        //    {
        //        ServiceProvider provider = _serviceProviderRepo.GetServiceProviderByEmail(checkoutSession.CustomerEmail);
        //        provider.SubData.StripeSubId = checkoutSession.SubscriptionId;
        //        provider.SubData.StripeId = checkoutSession.CustomerId;
        //        provider.SubData.IsPaid = true;
        //        _serviceProviderRepo.Update(provider);
        //        SubDataModel subDataModel = _subDataService.CreateModelForIndividualLookup(provider.SubData);
        //        _subDataService.Update(provider.SubData.Id, subDataModel);
        //    }

        //    return "Success";
        //}

        //public object? InvoicePaidHandler(Event stripeEvent)
        //{
        //    Invoice? invoice = stripeEvent.Data.Object as Invoice;

        //    if (invoice == null)
        //    {
        //        throw new Exception("Session Not Found");
        //    }
        //    else
        //    {
        //        ServiceProvider provider = _serviceProviderRepo.GetServiceProviderByEmail(invoice.CustomerEmail);
        //        provider.SubData.StripeId = invoice.CustomerId;
        //        provider.SubData.IsPaid = invoice.Paid;
        //        _serviceProviderRepo.Update(provider);
        //        SubDataModel subDataModel = _subDataService.CreateModelForIndividualLookup(provider.SubData);
        //        _subDataService.Update(provider.SubData.Id, subDataModel);
        //    }

        //    return "Success";
        //}

        //public object? InvoicePaymentFailedHandler(Event stripeEvent)
        //{
        //    Invoice? invoice = stripeEvent.Data.Object as Invoice;

        //    if (invoice == null)
        //    {
        //        throw new Exception("Session Not Found");
        //    }
        //    else
        //    {
        //        ServiceProvider provider = _serviceProviderRepo.GetServiceProviderByEmail(invoice.CustomerEmail);
        //        provider.SubData.StripeId = invoice.CustomerId;
        //        provider.SubData.IsPaid = false;
        //        _serviceProviderRepo.Update(provider);
        //        SubDataModel subDataModel = _subDataService.CreateModelForIndividualLookup(provider.SubData);
        //        _subDataService.Update(provider.SubData.Id, subDataModel);
        //    }

        //    return "Success";
        //}

        //public object? CanceledSubscriptionHandler(Event stripeEvent)
        //{
        //    Subscription? subscription = stripeEvent.Data.Object as Subscription;

        //    if (subscription == null)
        //    {
        //        throw new Exception("Session Not Found");
        //    }
        //    else
        //    {
        //        ServiceProvider provider = _serviceProviderRepo.GetProvideByStripeId(subscription.CustomerId);
        //        provider.SubData.StripeId = subscription.CustomerId;
        //        provider.SubData.IsPaid = false;
        //        _serviceProviderRepo.Update(provider);
        //        SubDataModel subDataModel = _subDataService.CreateModelForIndividualLookup(provider.SubData);
        //        _subDataService.Update(provider.SubData.Id, subDataModel);
        //    }

        //    return "Success";
        //}

        //public object? InvoiceHandler(Event stripeEvent)
        //{
        //    Invoice? invoice = stripeEvent.Data.Object as Invoice;

        //    if (invoice == null)
        //    {
        //        throw new Exception("Session Not Found");
        //    }
        //    else if(!invoice.Paid)
        //    {
        //        ServiceProvider provider = _serviceProviderRepo.GetServiceProviderByEmail(invoice.CustomerEmail);
        //        provider.SubData.StripeId = invoice.CustomerId;
        //        provider.SubData.StripeSubId = invoice.SubscriptionId;
        //        provider.SubData.IsPaid = false;
        //        _serviceProviderRepo.Update(provider);
        //        SubDataModel subDataModel = _subDataService.CreateModelForIndividualLookup(provider.SubData);
        //        _subDataService.Update(provider.SubData.Id, subDataModel);
        //    }

        //    return "Success";
        //}

        //public void CancelStripeSubNow(string stripeSubId)
        //{
        //    StripeConfiguration.ApiKey = _configuration["StripeAPIKey"];

        //    try
        //    {
        //        var service = new SubscriptionService();
        //        var options = new SubscriptionUpdateOptions
        //        {
        //            CancelAtPeriodEnd = false,
        //        };
        //        Subscription subscription = service.Get(stripeSubId);

        //        if(subscription.Status != "canceled" && subscription.Status != "incomplete_expired")
        //        {
        //            subscription = service.Update(stripeSubId, options);
        //        }
        //    }
        //    catch
        //    {
        //        throw new Exception("Sub failed to cancel");
        //    }

        //}

        //public void CancelStripeSubAtEndOfBillingCycle(string stripeSubId)
        //{
        //    StripeConfiguration.ApiKey = _configuration["StripeAPIKey"];

        //    try
        //    {
        //        var service = new SubscriptionService();
        //        var options = new SubscriptionUpdateOptions
        //        {
        //            CancelAtPeriodEnd = true,
        //        };
        //        Subscription subscription = service.Update(stripeSubId, options);
        //    }
        //    catch
        //    {
        //        throw new Exception("Sub failed to cancel");
        //    }
        //}

        //public void DeleteStipeCustomer(string customerId)
        //{
        //    var service = new CustomerService();
        //    var customer = service.Get(customerId);

        //    if (customer.Deleted == false)
        //    {
        //        service.Delete(customerId);
        //    }
        //}
    }
}
