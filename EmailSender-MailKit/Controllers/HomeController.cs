using EmailSender_MailKit.Models;
using SendEmail.Contracts;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SendEmail.Struct;

namespace EmailSender_MailKit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //DI
        private readonly IEmailSenderService _EmailSenderService;
        public HomeController(ILogger<HomeController> logger, IEmailSenderService emailSenderService)
        {
            _logger = logger;
            _EmailSenderService = emailSenderService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //Don't Forget add SendEmail to project refrence and add service to Program.cs
        public async Task<IActionResult> sendEmail()
        {
            //Use Partial View For Body (We Need Partial Convertor to String)
            string body = PartialToString.RenderViewToString(this, "_EmailBody");
            await _EmailSenderService.SendEmailAsync("recevier Email", "Subject", body);
            return Content("Sended");
        }
    }
}