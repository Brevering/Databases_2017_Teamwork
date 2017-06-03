using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesSystem.Models
{
    public class Genre
    {
        public Genre()
        {
            this.Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }

        [MaxLength(20)]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
