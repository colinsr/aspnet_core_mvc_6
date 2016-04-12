using Microsoft.AspNet.Mvc;

namespace TheWorld_V2.Controllers.Web
{
    public class AppController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
