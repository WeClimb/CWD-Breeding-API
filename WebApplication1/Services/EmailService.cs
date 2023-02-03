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
            var sendingEmail = _configuration["SendGridMailSettings:DomainName"];
            var sendingName = _configuration["SendGridMailSettings:SenderName"];
            var client = new SendGridClient(apiKey);

            var sendGridFrom = new EmailAddress(sendingEmail, sendingName);
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
    }
}
