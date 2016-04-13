using System.Linq;
using Microsoft.AspNet.Mvc;
using TheWorld_V2.Models;
using TheWorld_V2.Services;
using TheWorld_V2.ViewModels;

namespace TheWorld_V2.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;
        private readonly WorldContext _context;

        public AppController(IMailService mailService, WorldContext context)
        {
            _mailService = mailService;
            _context = context;
        }

        public IActionResult Index()
        {
            var trips = _context.Trips.OrderBy(t => t.Name).ToList();

            return View(trips);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (ModelState.IsValid)
            {
                var email = Startup.Configuration["AppSettings:SiteEmailAddress"];

                if (string.IsNullOrWhiteSpace(email))
                    //empty string as first param translates to obj level error
                    ModelState.AddModelError("", "Could not send email configuration problem.");

                if (_mailService.SendMail(email, email, $"Contact page from {model.Name} : {model.Email}", model.Message))
                {
                    ModelState.Clear();//this will clear the form after post...
                    ViewBag.Message = "Mail sent, thank you!";
                }
            }
            
            return View();
        }
    }
}
