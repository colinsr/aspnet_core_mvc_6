using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TheWorld_V2.Models
{
    public class WorldUser : IdentityUser
    {
        public DateTime FirstTrip { get; set; }
    }
}