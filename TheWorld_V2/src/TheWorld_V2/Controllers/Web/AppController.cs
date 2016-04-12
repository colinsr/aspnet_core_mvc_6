using Microsoft.AspNet.Mvc;
using TheWorld_V2.Services;
using TheWorld_V2.ViewModels;

namespace TheWorld_V2.Controllers.Web
{
    public class AppController : Controller
    {
        private readonly IMailService _mailService;

        public AppController(IMailService mailService)
        {
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            return View();
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
            _mailService.SendMail("", "", $"Contact page from {model.Name}", model.Message);

            return View();
        }
    }
}
