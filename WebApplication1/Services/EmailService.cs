using SendGrid;
using SendGrid.Helpers.Mail;

namespace ReviewPlatformAPI.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool SendEmail(string to, string subject, string body, List<string>? ccs)
        {
#if DEBUG
            to = "weclimbsoftware@gmail.com";

            if (ccs != null)
            {
                ccs = ccs.Select(x => x = "weclimbdevtesting@gmail.com").ToList();
            }
#endif

            return SendViaSendGrid(to, subject, body, ccs);
        }

        private bool SendViaSendGrid(string to, string subject, string body, List<string>? ccs)
        {
            var apiKey = _configuration["SendGridMailSettings:ApiKey"];
            var client = new SendGridClient(apiKey);

            var sendGridFrom = new EmailAddress("weclimbdevtesting@gmail.com", "WeClimbS");
            var sendGridTo = new EmailAddress(to);
            var sendGridCC = new List<EmailAddress>();
            var htmlContent = body;

            if (ccs != null)
            {
                foreach (var cc in ccs)
                {
                    sendGridCC.Add(new EmailAddress(cc));
                }
            }

            var msg = new SendGridMessage()
            {
                From = sendGridFrom,
                Subject = subject,
                HtmlContent = htmlContent,
                Personalizations = new List<Personalization>()
                {
                    new Personalization()
                    {
                        Tos = new List<EmailAddress>()  {sendGridTo }
                    }
                }
            };

            if (sendGridCC.Any())
            {
                msg.Personalizations[0].Ccs = sendGridCC;
            }

            var response = client.SendEmailAsync(msg).Result;

            return response.IsSuccessStatusCode;
        }

        //private bool SendSMTP(string from, string to, string subject, string body, List<string> ccs)
        //{
        //    string emailUsername = _configuration["EMAIL_USERNAME"];
        //    string emailPassword = _configuration["EMAIL_PASSWORD"];
        //    string emailConfiguration = _configuration["EMAIL_CONFIGURATION"];

        //    var smtpClient = new SmtpClient(emailConfiguration)
        //    {
        //        Port = 587,
        //        DeliveryMethod = SmtpDeliveryMethod.Network,
        //        Credentials = new NetworkCredential(emailUsername, emailPassword),
        //        EnableSsl = true
        //    };

        //    try
        //    {
        //        MailMessage message = new MailMessage();
        //        message.From = new MailAddress(from);
        //        message.To.Add(new MailAddress(to));
        //        message.Subject = subject;
        //        message.Body = body;
        //        message.IsBodyHtml = true;
        //        message.ReplyToList.Add(new MailAddress(from));

        //        if (ccs != null)
        //        {
        //            foreach (var cc in ccs)
        //            {
        //                message.CC.Add(cc);
        //            }
        //        }

        //        smtpClient.Send(message);
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //    return true;
        //}
    }
}
