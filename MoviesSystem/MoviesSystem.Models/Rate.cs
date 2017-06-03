using System.ComponentModel.DataAnnotations;

namespace MoviesSystem.Models
{
    public class Rate
    {
        public int Id { get; set; }

        [Range(1,10)]
        public int RateValue { get; set; }
    }
}
