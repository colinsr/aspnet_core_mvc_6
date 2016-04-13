﻿using System;

namespace TheWorld_V2.ViewModels
{
    public class StopViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Arrival { get; set; }
    }
}