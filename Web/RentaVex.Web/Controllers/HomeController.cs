namespace RentaVex.Web.Controllers
{
    using System;
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using RentaVex.Services.Data;
    using RentaVex.Web.ViewModels;

    public class HomeController : Controller
    {
        private readonly IGetCountService countService;

        public HomeController(IGetCountService countService)
        {
            this.countService = countService;
        }

        public IActionResult Index()
        {
            var viewModel = this.countService.GetCounts();
            return this.View(viewModel);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult ContactUs(int id)
        {
            //return this.View(new ContactFormViewModel());
            return this.View();
        }

        //[HttpPost]
        //public IActionResult ContactUs(ContactFormViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    try
        //    {
        //        // Configure SMTP settings (You can also read these settings from configuration)
        //        string smtpServer = "your_smtp_server";
        //        int smtpPort = 587; // Or any other port
        //        string smtpUsername = "your_smtp_username";
        //        string smtpPassword = "your_smtp_password";
        //        string senderEmail = "your_sender_email";
        //        string receiverEmail = "your_receiver_email";

        //        using (var client = new SmtpClient(smtpServer, smtpPort))
        //        {
        //            client.UseDefaultCredentials = false;
        //            client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
        //            client.EnableSsl = true;

        //            var message = new MailMessage();
        //            message.From = new MailAddress(senderEmail);
        //            message.To.Add(receiverEmail);
        //            message.Subject = model.Subject;
        //            message.Body = $"Name: {model.Name}\nEmail: {model.Email}\n\n{model.Message}";

        //            client.Send(message);
        //        }

        //        ViewData["Message"] = "Your message has been sent successfully!";
        //        return View();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Log the error
        //        ViewData["Error"] = "An error occurred while sending your message. Please try again later.";
        //        return View(model);
        //    }
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
