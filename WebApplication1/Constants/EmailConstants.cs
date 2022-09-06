namespace ReviewPlatformAPI.Constants
{
 
    //TODO: Get rid of all non used emails and set up for PreKno
    public class EmailConstants
    {
        public static string ReplyToEmail = "onTheUpper@outlook.com";

        public static List<string> EmployeeCC = new List<string>() {};

        // subjects
        public static string ChangePasswordSubject = "Upper Account - Set-up/Change Password";

        public static string ApplicationSubmittedSubject = "Application Ready for Review";

        public static string ApplicationApprovedSubject = "Application Approved";

        public static string ApplicationDeniedSubject = "Application Denied";

        public static string ApplicationNeedsWorkSubject = "Application Needs Work";

        public static string ApplicationEmailVerificationSubject = "Please, Verify Your Email Address";

        public static string ProvidersNoticeOfNewMemberSubject = "New Member Notification";

        public static string MemberNoticeOfPlanFinilizationSubject = "Notice of Plan Finalization";

        public static string MemberMonthlyWithdrawReminderSubject = "Friendly Reminder of Withdraw";

        public static string MemberSuccessfulChargeSubject = "Charge was Successful";

        public static string ProviderSuccessfulChargeSubject = "Members Charge was Successful";

        public static string MemberFailureOfChargeSubject = "Payment Failure";

        public static string ProviderFailureOfChargeSubject = "Payment Failure";

        public static string ACHSubject = "Payment Set Up For Your Plan!";

        public static string CustomerPaymentSuccessSubject = "CUSTOMER CONFIRMATION EMAIL";

        public static string ProviderNewOrderSubject = "PROVIDER CONFIRMATION EMAIL";

        public static string ProviderWelcomeSubject = "Welcome To Upper!";

        public static string CustomerNewSubscriptionSubject = "You're on the Up(per)!";

        public static string CustomerCancelSubscriptionSubject = "We're Sorry You're Down :(";

        public static string ProviderNewSubscriptionSubject = "You're Popular on Upper: New Subscription Commitment";

        public static string ProviderCustomerCancelsSubscriptionSubject = "Customer Cancellation: This is an Opportunity for Growth!";

        public static string CustomerProviderCancelsSubscriptionSubject = "Your Subscription has been Discontinued";


        public static string ChangePasswordBody = "{0},<br><br>A request has been sent to set-up/change your password for your " +
                                                    "Upper Account,   " +
                                                    "if this was you click <a href='{1}'>here</a> " +
                                                    "to reset your password.<br><br>If this was not you, please ignore this email.";

        public static string MemberApplicationSubmittedBody = "{0},<br><br>Great move. You found a huge opportunity and took the first step by submitting your application." +
                                                                " Our system already generated an answer that is pending for you and now our team will just need to review it." +
                                                                " You will hear from us in less than 24 hours!<br><br>" +
                                                                "Click <a href='{1}'>here</a> to review the application.<br><br> " +
                                                                "If you have any questions until then, you can reach us at (910)-800-0287 " +
                                                                "or by email at info@healthpossibleinc.org.";

        public static string ProviderApplicationApprovedBody = "Hey {0},<br><br>" +
                                                               "Congrats, you're on the Upper! Thank you so much for joining us. We are beyond excited to get your services to the world and we know how many people's lives will improve because of you.<br><br>" +
                                                               "Be sure to <a href='{1}'>login</a> here and double check your services - they're for sale! Or, want to see them? <a href='{2}'>Start shopping!</a>";

        public static string ProviderApplicationDeniedBody = "{0},<br><br>We know how much you want to help others, so listen, we have bad news and good news. " +
                                                                "The bad news is that you do not qualify to be on the Uppers Provider Network at the moment, " +
                                                                "but the good news is that you are taking action to get your services to many, " +
                                                                "and there are tons of other ways we can come together and make a difference.<br.<br>" +
                                                                "Someone on our team will be in touch with you within 48 hours about your application, " +
                                                                "why it was declined, if we can fix it, and in what ways we can still work together. " +
                                                                "Don’t go anywhere, because you’re awesome, and we appreciate your support.";


        public static string MemberApplicationApprovedBody = "{0},<br><br>Congratulations!<br><br>You’ve been approved to build your very own Custom Wellness Plan™ - " +
                                                                "a holistic healthcare plan that prioritizes wellness, is affordable, and keeps all of your needs and " +
                                                                "payments on one single platform.<br><br>" +
                                                                "It’s time to log back in to your account, build your plan, and then sign your contract.  Come on, let’s go!!<br><br>" +
                                                                "Click <a href='{1}'>here</a> to access the generated contract.";

        public static string MemberApplicationDeniedBody = "{0},<br><br>We know how much you want to change, so listen, we have bad news and good news. " +
                                                                "The bad news is that you do not qualify for Health Possible at the moment, but the good news is " +
                                                                "that you make too much money and can afford care without a discount (that’s a good problem to have)!<br><br> " +
                                                                "We’ve run into this before, so our Founder opened another company for people in your exact shoes, " +
                                                                "called Upper.  They do almost the same thing.  Upper has some variations that allow you to build a " +
                                                                "holistic healthcare plan affordable for your own budget, but with or without qualifying for a discount. " +
                                                                "You’ll see.  They also sponsor Health Possible, so we’re partners!<br><br>" +
                                                                "Please visit <a href = 'www.ontheupper.com'> www.ontheupper.com</a> to continue on this awesome journey you’ve started and commit to getting the wellness care " +
                                                                "that you need.";


        public static string ApplicationNeedsWorkBody = "{0},<br><br>Your application needs work. Please, <a href='{1}'>log back into</a> your Upper account to finish your application.";

        public static string ApplicationEmailVerificationBody = "{0},<br><br>We hear you are looking to join Health Possible!  " +
                                                                "Please verify your email so we can let you in.<br><br>" +
                                                                "Click <a href='{1}'>here</a> to verify email.<br><br>" +
                                                                "Follow us on social media to always be included.<br><br>" +
                                                                "[Instagram, Facebook, LinkedIn, Twitter links here]";

        public static string NewMemberNoticeBody = "Our newest Member, {0}, has chosen you for their Custom Wellness Plan™! " +
                                                                "We’re glad you are building your business through Health Possible<br><br>" +
                                                                "Here are the details:<br>" +
                                                                "Name: {0}<br>" +
                                                                "Phone: {1}<br>" +
                                                                "Email Address: {2}<br><br>" +
                                                                "Qualified For: {3}<br><br>" +
                                                                "{4} sessions, {5}, per month, for {6} months total.<br><br>" +
                                                                "It’s time for you, the accountability leader, to call {0} and get them scheduled. " +
                                                                "We encourage you to get {0}’s (email) written permission to talk to any other Providers " +
                                                                "they might have chosen so that you can reach out and collaboratively help the member together. " +
                                                                "Thank you for providing quality service and we look forward to seeing {0}’s progress.";

        public static string MemberNoticeOfPlanFinalBody = "Glad you are doing so well, and welcome to the family!<br><br>" +
                                                                "Remember on your journey to “never give up what you want most for what you want now.” " +
                                                                "There will be temptations, but you’re in this for the long haul and THIS IS NOT A SPRINT. " +
                                                                "Make great, sustainable decisions.<br><br>" +
                                                                "We gave all of your chosen Providers your Custom Wellness Plan™ details and asked them to " +
                                                                "contact you to begin scheduling.  However, if you really want to impress them with your " +
                                                                "commitment level, call them first and ask for your first appointment. " +
                                                                "Either way is fine!<br><br>" +
                                                                "You can find your Provider(s) contact information in your contract.<br><br>" +
                                                                "We will periodically send you a Personal Health Inventory to complete so you can keep setting " +
                                                                "new goals and measuring your progress.";

        public static string MemberMonthlyWithdrawRemindBody = "This is a friendly reminder that your account will be drafted for {0} on {1}. " +
                                                                "Set a reminder to make sure the funds available on time, otherwise you will not be " +
                                                                "able to attend any care!  Thank you for your commitment, prioritizing your own well-being, " +
                                                                "and respecting our providers.";

        public static string MemberSuccessfulChargeBody = "Success!  Your Health Possible payment went through, which means you can attend care for " +
                                                                "another 30 days.  Make your health your priority and schedule this month’s appointments " +
                                                                "ahead of time!";

        public static string ProviderSuccessfulChargeBody = "Success! {0}’s Health Possible payment went through, which means you can schedule " +
                                                                "their care for another 30 days.  Schedule this month’s appointments ahead of time to " +
                                                                "make them a priority!";

        public static string MemberFailureOfChargeBody = "Unfortunately, your Health Possible payment did not go through, which means you cannot schedule any " +
                                                                "future or unpaid appointments until it does.  We will try again in 24 hours or you can make the " +
                                                                "funds available, login to your account, and manually make the payment now.<br><br>" +
                                                                "If you are having trouble making your payment, please call, text, or email us about our free " +
                                                                "financial health counseling.  We partner with banks who volunteer to teach our members " +
                                                                "about personal finance, budgeting, and credit repair. " +
                                                                "You can also login and change-up your Plan.<br><br>" +
                                                                "We get it, and we’re entirely here for you.";

        public static string ProviderFailureOfChargeBody = "Unfortunately, {0}’s Health Possible payment did not go through today. " +
                                                                "Therefore, you may NOT schedule any future or unpaid appointments with them until it does. " +
                                                                "We are protecting your time with this email notice.<br><br>" +
                                                                "We send this IMPORTANT notice to let you know that if you disregard this email and schedule " +
                                                                "{0} without successful payment, that Health Possible nor the listed member " +
                                                                "are responsible for any costs incurred.  Don’t say we didn’t warn you!<br><br>" +
                                                                "We’ve automatically offered this member free financial health counseling " +
                                                                "(just so you know that we are making strides to keep them going), and we will let you " +
                                                                "know once {0}’s payment successfully goes through.<br><br>" +
                                                                "Thank you for being patient!";

        public static string MemberACHBody = "Congrats {0}, <br><br>" +
                                                                "Here is a link to set up payment for your new plan!<br><br>" +
                                                                "Click <a href='{1}'>here</a> to access the generated contract.";

        public static string CustomerPaymentSuccessBody = "You're on the Upper!  Congratulations. " +
                                                                "Improving your well-being takes one step at a time, and the best part is that natural high you get from peak success during your appointments, as well as after them.<br><br>" +
                                                                "Upper is a BRAND NEW company, so we are still developing your ability to purchase services via their exact dates and times. " +
                                                                "For now, we're putting you directly in contact with your provider(s). " +
                                                                "Here is your providers info to simply call or email to schedule your appointment(s):<br><br>---<br><br>" +
                                                                "{0}---<br><br>" +
                                                                "Note: We've also told your provider(s) to reach out to YOU!  They get an email like this too.  So whoever contacts who first [to schedule] wins! :)" +
                                                                "Thanks for supporting our brand new business, we look forward to getting you on the Upper forever-more.";

        public static string CustomerPaymentSuccessBusinesses = "Plan: {0}<br>" +
                                                                "Type: {1}<br>" +
                                                                "Location: {2}<br>" +
                                                                "Business: {3}<br>" +
                                                                "Address: {4}<br>" +
                                                                "Purchased Care is Managed By: {5}<br>" +
                                                                "Amount Purchased: {6}" +
                                                                "Email: {7}<br>" +
                                                                "Phone: {8}";

        public static string ProviderNewOrderBody = "You got a customer on the Upper!  Congratulations. " +
                                                                "Improving your business takes one customer at a time, and we're here to help you deliver.<br><br>" +
                                                                "Upper is a BRAND NEW company, so we are still developing shoppers ability to purchase your services via your exact available dates and times. " +
                                                                "For now, we're putting you directly in contact with your customer. " +
                                                                "Please contact your customer via phone call within 24 hours on the current or next business day for optimal customer service and customer satisfaction. " +
                                                                "Be aware, someone might be confused if they don't hear from you pretty quickly.  Fast always wins (and impresses people).<br><br>" +
                                                                "Here is your customers contact information to simply call them and schedule their appointment(s):<br><br>---<br><br>" +
                                                                "Customer Name: {0}<br>" +
                                                                "Email: {1}<br>" +
                                                                "Phone: {2}<br><br>" +
                                                                "They Purchased: {3} of {4}<br>" +
                                                                "Location: {5}<br>" +
                                                                "At the amount of: {6}<br><br>---<br><br>" +
                                                                "Note: We invited your customer to contact you to schedule as well, but we prefer if our professionals take that lead. " +
                                                                "We will text your customer within 48 hours to confirm successful contact with you and that the appointment has been scheduled before we process your payments.<br><br>" +
                                                                "Thanks for joining Upper, we look forward to helping your business grow!";

        public static string ProviderWelcomeBody = "Hey {0}!<br><br>" +
                                                                "Thank you so much for getting on the Upper platform, we are so excited to work with you.  " +
                                                                "My name is Sara, Founder/CEO of Upper, and I just wanted to personally thank you as well as introduce you to our Relationship Managers here, Jeremy and Dan.  " +
                                                                "If you need any help during the application process, or support when your services go live, all three of us are available right here by email.  " +
                                                                "You can also call or text us via our team phone line at (910) 207-0122.<br><br>" +
                                                                "We look forward to growing your business through our technology.";

        public static string CustomerNewSubscriptionBody = "We are grateful that you chose to invest in yourself and your health consistently each month with a new monthly subscription.  " +
                                                                "You can go ahead and start taking advantage of your new membership right away.  If you need to contact the provider to schedule any appointments, find out how to book classes, etc., please do so by contacting them:<br>" +
                                                                "{0}<br>" +
                                                                "{1}<br>" +
                                                                "{2}<br><br>" +
                                                                "This provider's business address is:<br>" +
                                                                "{3}<br><br>" +
                                                                "Please do not hesitate to contact our team at Upper if you need any help coordinating.You can reach us at (910) 207-0122 or <a href='mailto:get@ontheupper.com'>get@ontheupper.com</a>.";

        public static string CustomerCancelSubscriptionBody = "It's never fun coming off the Upper...  But, we know things will get better and we hope that you'll be back up again soon.  " +
                                                                "If there is something you'd like to participate in, search for, or sign up for instead of this canceled subscription, let us know!  " +
                                                                "We're happy to help you shop our site and find the right care for you, any time.<br><br>" +
                                                                "You canceled your subscription with {0} in {1}<br><br>" +
                                                                "Please do not hesitate to contact our team at Upper if you need any help trying something new. You can reach us at(910) 207-0122 or <a href='mailto:get@ontheupper.com'>get@ontheupper.com</a>.";

        public static string ProviderNewSubscriptionBody = "We are grateful that you chose to sell your services here and get more people on the Upper.  We let your new customer know that they can begin taking advantage of their new membership with you.  " +
                                                                "It is great customer service if you be the first to call them and get them scheduled - this shows that you really care about them and their journey.<br><br>" +
                                                                "Here is your new customer:<br>" +
                                                                "{0}<br>" +
                                                                "{1}<br>" +
                                                                "{2}<br><br>" +
                                                                "They purchased:<br>" +
                                                                "{3}<br><br>" +
                                                                "Please do not hesitate to contact our team at Upper if you need any help coordinating.You can reach us at(910) 207-0122 or <a href='mailto:get@ontheupper.com'>get@ontheupper.com</a>.";

        public static string ProviderCustomerCancelsSubscriptionBody = "Your customer, {0} just canceled their subscription service, {1}, with you.  " +
                                                                       "There are a few reasons that this could be, and we always want to face these things head-on and come up with solutions that might help you:<br><br>" +
                                                                       "<ul>" +
                                                                       "<li>You changed their life and they are growing up to even bigger things, a next level!</li>" +
                                                                       "<li>The relationship didn't mesh :(</li>" +
                                                                       "<li>Price, affordability, and/or perceived value of membership</li>" +
                                                                       "<li>Scheduling Issues</li>" +
                                                                       "<li>Quality of Care and/or Lack of Results</li>" +
                                                                       "<li>Changing from one of your services to another?</li>" +
                                                                       "<li>Other</li>" +
                                                                       "</ul><br>" +
                                                                       "Whatever it is, right or wrong, this is our opportunity to learn and improve your business together.There is a method of improvement, no matter what their reasoning.<br><br>" +
                                                                       "One of our representatives will be in touch to better understand your experience with this customer, and develop solutions that can improve your business and the next customer.<br><br>" +
                                                                       "Please do not hesitate to contact our team at Upper, first, if you have questions.You can reach us at (910) 207-0122 or <a href='mailto:get@ontheupper.com'>get@ontheupper.com</a>.";

        public static string CustomerProviderCancelsSubscriptionBody = "Your provider, {0} just discontinued one of their services that you've been subscribed to: {1}.  " +
                                                                       "We're letting you know because when this happens, we immediately cancel your Upper subscription, so you won't be charged anymore for something you are not getting.  No sweat!<br><br>" +
                                                                       "There are a lot of reasons a provider might discontinue a service, so we urge you to speak with them directly with any questions. Should you feel compelled to offer them your continued support in other ways, we guarantee they would appreciate it!<br><br>" +
                                                                       "One of our representatives will be in touch to better understand your experience with this provider, as well as find you a replacement service so you can stay healthy and on the Upper.<br><br>" +
                                                                       "Please do not hesitate to contact our team at Upper, first, if you have questions.You can reach us at (910) 207-0122 or <a href='mailto:get@ontheupper.com'>get@ontheupper.com</a>.";

    }
}
