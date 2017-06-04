using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesSystem.Models
{
    public class Actor
    {
        public Actor()
        {
            this.Movies = new HashSet<Movie>();
        }

        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(30)]
        [Required]
        public string LastName { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}
