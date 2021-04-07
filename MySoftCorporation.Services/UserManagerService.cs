using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using MySoft.Employee.Entities;
using MySoftCorporation.Data.Entities;
using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web;

namespace MySoftCorporation.Services
{
    public class EmailService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            await Task.Factory.StartNew(() =>
            sendMail(message)
            );
        }

        private void sendMail(IdentityMessage message)
        {
            string text = string.Format("Please click on this link to {0}: {1}", message.Subject, message.Body);
            string html = "Please confirm your account by clicking this link: <a href=\"" + message.Body + "\">link</a><br/>";
            html += HttpUtility.HtmlEncode(@"Or click on the copy the following link on the browser:" + message.Body);
            string EmailAddress = ConfigurationManager.AppSettings["Email"].ToString();
            string Password = ConfigurationManager.AppSettings["Password"].ToString();
            MailMessage msg = new MailMessage
            {
                From = new MailAddress(EmailAddress)
            };
            msg.To.Add(new MailAddress(message.Destination));
            msg.Subject = message.Subject;
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, null, MediaTypeNames.Text.Plain));
            msg.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(html, null, MediaTypeNames.Text.Html));
            NetworkCredential credentials = new NetworkCredential(EmailAddress, Password);
            SmtpClient smtpClient = new SmtpClient("mail.mysoftcorporation.pk", Convert.ToInt32(25))
            {
                UseDefaultCredentials = true,
                Credentials = credentials,
                EnableSsl = false
            };
            smtpClient.Send(msg);
        }
    }
    public class UserManagerService : UserManager<User>
    {
        public UserManagerService(IUserStore<User> store)
           : base(store)
        {
        }

        public static UserManagerService Create(IdentityFactoryOptions<UserManagerService> options, IOwinContext context)
        {
            var manager = new UserManagerService(new UserStore<User>(context.Get<MySoftCorporationDbContext>()));
            // Configure validation logic for usernames
            manager.UserValidator = new UserValidator<User>(manager)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            // Configure validation logic for passwords
            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
            };

            // Configure user lockout defaults
            manager.UserLockoutEnabledByDefault = true;
            manager.DefaultAccountLockoutTimeSpan = TimeSpan.FromMinutes(5);
            manager.MaxFailedAccessAttemptsBeforeLockout = 5;

            // Register two factor authentication providers. This application uses Phone and Emails as a step of receiving a code for verifying the user
            // You can write your own provider and plug it in here.
            manager.RegisterTwoFactorProvider("Phone Code", new PhoneNumberTokenProvider<User>
            {
                MessageFormat = "Your security code is {0}"
            });
            manager.RegisterTwoFactorProvider("Email Code", new EmailTokenProvider<User>
            {
                Subject = "Security Code",
                BodyFormat = "Your security code is {0}"
            });
            manager.EmailService = new EmailService();
            //manager.SmsService = new SmsService();
            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                manager.UserTokenProvider =
                    new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"));
            }
            return manager;
        }
    }
}