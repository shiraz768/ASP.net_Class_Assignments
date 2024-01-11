using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Mail;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace Shiraz.Controllers
{
    public class EmailController : Controller
    {
        //Global Declarations
        string mailbody = @"
    <html>
    <head>
        <title>Your Email Title</title>
    </head>
    <body>
        <h1 style='color:white;background-color:orange'>Hello!</h1>
        <p  style='color:gray;background-color:orange'>This is an example email content in HTML format.</p>
        <p  style='color:gray;background-color:orange'>You can customize this with your own message, formatting, and styles.</p>
        <p  style='color:gray;background-color:orange'>Feel free to add images, links, and more HTML elements as needed.</p>
    </body>
    </html>
";
        string FromEmail = "";
        string mailtitle = "Attachment Demo";
        string mailsubject = "Email with Attachment";
        string mailpassword = "";

        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(string ToEmail, IFormFile fileToAttach)
        {
            //Mail Message
            MailMessage message = new MailMessage(new MailAddress(FromEmail, mailtitle), new MailAddress(ToEmail));
            //Mail Content
            message.Subject = mailsubject;
            message.Body = mailbody;
            message.IsBodyHtml = true;
            //Attachment
            message.Attachments.Add(new Attachment(fileToAttach.OpenReadStream(), fileToAttach.FileName));
            //Server Details
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            //Credentials
            System.Net.NetworkCredential credential = new System.Net.NetworkCredential();
            credential.UserName = FromEmail;
            credential.Password = mailpassword;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = credential;
            //SendEmail
            smtp.Send(message);
            ViewBag.emailsentmessage = "Email Sent Successfully";
            return View();
        }

    }
}




