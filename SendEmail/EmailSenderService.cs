using SendEmail.Contracts;
using MimeKit;
using MimeKit.Text;
using MailKit.Security;
//SmtpClient
//using System.Net.Mail; Microsoft has deprecated it 
using MailKit.Net.Smtp;

namespace SendEmail
{
    public class EmailSenderService : IEmailSenderService
    {
        public async Task SendEmailAsync(string to, string subject, string body)
        {
            //Create Message
            var Message = new MimeMessage();
            //From Who                             Sender Name        ,OurWebsite address(It isn't shown in the Gmail service)              
            Message.From.Add(new MailboxAddress("Our Site Title", "sender@example.com"));
            //to who                     Name of reciever,Email of reciever
            Message.To.Add(new MailboxAddress("Test", to));
            //Subject of email
            Message.Subject = subject;
            //Body of the email support Html
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            Message.Body = bodyBuilder.ToMessageBody();

            #region Smtp Client

            using var client = new SmtpClient();

            //Some email servers require you to enter this(Your website Name)
            //client.LocalDomain = "MyWebSite";

            //connect to Email server host(smtp.gmail.com),port(465 if use ssl) , ssl(true,false)
            await client.ConnectAsync("smtp.example.com", 465, true);
            //our gmail account                          Or your app password(for more information read the pdf)             
            await client.AuthenticateAsync("username", "password");
            //send email
            await client.SendAsync(Message);
            //dispose our method(disconnect our connection)
            await client.DisconnectAsync(true);

            #endregion
        }
    }
}