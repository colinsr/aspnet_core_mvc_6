using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
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
            var trips = Mapper.Map<IEnumerable<TripViewModel>>(_repo.GetAllTripsWithStops());

            return Json(trips);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newTrip = Mapper.Map<Trip>(viewModel);

                    //save to db.

                    Response.StatusCode = (int) HttpStatusCode.Created;
                    return Json( Mapper.Map<TripViewModel>(newTrip) );
                }
            }
            catch (Exception ex)
            {
               Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = ex.Message, ModelState = ModelState }); 
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }
    }
}
