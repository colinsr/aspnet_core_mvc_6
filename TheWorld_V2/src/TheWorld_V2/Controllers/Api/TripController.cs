using System.Net;
using Microsoft.AspNet.Mvc;
using TheWorld_V2.Models;
using TheWorld_V2.ViewModels;

namespace TheWorld_V2.Controllers.Api
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        private readonly IWorldRepository _repo;

        public TripController(IWorldRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var trips = _repo.GetAllTripsWithStops();

            return Json(trips);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel newTrip)
        {
            if (ModelState.IsValid)
            {
                Response.StatusCode = (int) HttpStatusCode.Created;
                return Json(true);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }
    }
}
