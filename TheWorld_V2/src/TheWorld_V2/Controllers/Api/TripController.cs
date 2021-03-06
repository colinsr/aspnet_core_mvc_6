﻿using System;
using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld_V2.Models;
using TheWorld_V2.ViewModels;

namespace TheWorld_V2.Controllers.Api
{
    [Authorize]
    [Route("api/trips")]
    public class TripController : Controller
    {
        private readonly IWorldRepository _repo;
        private readonly ILogger<TripController> _logger;

        public TripController(IWorldRepository repo, ILogger<TripController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpGet("")]
        public JsonResult Get()
        {
            var trips = Mapper.Map<IEnumerable<TripViewModel>>(_repo.GetUserTripsWithStops("colin"));//User.Identity.Name));

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

                    newTrip.UserName = User.Identity.Name;

                    //save to db.
                    _logger.LogInformation("Attempting to save new trip into the database");
                    _repo.AddTrip(newTrip);

                    if (_repo.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(newTrip));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new trip", ex);
               Response.StatusCode = (int)HttpStatusCode.BadRequest;
               return Json(new { Message = ex.Message, ModelState = ModelState }); 
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json(new { Message = "Failed", ModelState = ModelState });
        }
    }
}
