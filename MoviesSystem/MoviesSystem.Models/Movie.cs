using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesSystem.Models
{
    public class Movie
    {
        public Movie()
        {
            this.Genres = new HashSet<Genre>();
            this.Actors = new HashSet<Actor>();
        }

        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        public string Title { get; set; }

        public virtual ICollection<Genre> Genres { get; set; }

        public virtual ICollection<Actor> Actors { get; set; }

        public virtual Description Description { get; set; }

        public virtual Rate Rate { get; set; }
    }
}
