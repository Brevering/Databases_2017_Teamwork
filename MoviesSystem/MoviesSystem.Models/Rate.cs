﻿using System.ComponentModel.DataAnnotations;

namespace MoviesSystem.Models
{
    public class Rate
    {
        public int Id { get; set; }

        [Range(0,10)]
        public float RateValue { get; set; }
    }
}
