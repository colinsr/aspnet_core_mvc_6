using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld_V2.Models;
using TheWorld_V2.Services;
using TheWorld_V2.ViewModels;

namespace TheWorld_V2.Controllers.Api
{
    [Authorize]
    [Route("api/trips/{tripName}/stops")]
    public class StopController : Controller
    {
        private readonly IWorldRepository _repo;
        private readonly ILogger<StopController> _logger;
        private readonly GeoService _geoService;

        public StopController(IWorldRepository repo, ILogger<StopController> logger, GeoService geoService)
        {
            _repo = repo;
            _logger = logger;
            _geoService = geoService;
        }

        [HttpGet("")]
        public JsonResult Get(string tripName)
        {
            try
            {
                var trip = _repo.GetTripByName(tripName, User.Identity.Name);

                if (trip == null)
                    return Json(null);

                return Json( Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(stop => stop.Order)) );

            }
            catch ( Exception ex )
            {
               _logger.LogError($"Failed to get stops for trip {tripName}.", ex);
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json( "Error occured finding trip name." );
            }
        }

        [HttpPost("")]
        public async Task<JsonResult> Post( string tripName, [FromBody] StopViewModel viewModel )
        {
            try
            {
                if ( ModelState.IsValid )
                {
                    var newStop = Mapper.Map<Stop>( viewModel );

                    var geo = await _geoService.Lookup(viewModel.Name);

                    if (!geo.Success)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(geo.Message);
                    }

                    newStop.Latitude = geo.Latitude;
                    newStop.Longitude = geo.Longitude;

                    _repo.AddStop( tripName, newStop, User.Identity.Name );

                    if ( _repo.SaveAll() )
                    {
                        Response.StatusCode = ( int ) HttpStatusCode.Created;
                        return Json( Mapper.Map<StopViewModel>(newStop) );                        
                    }

                }
            }
            catch ( Exception ex )
            {
                _logger.LogError( "Failed to save new stop", ex );
                Response.StatusCode = (int) HttpStatusCode.BadRequest;
                return Json( "Failed to save new stop!" );
            }

            return Json( "Validation failed on new stop" );
        }
    }
}