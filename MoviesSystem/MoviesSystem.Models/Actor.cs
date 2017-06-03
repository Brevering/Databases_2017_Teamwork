using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MoviesSystem.Models
{
    public class Actor
    {
        public Actor()
        {
            this.Descriptions = new HashSet<Description>();
        }

        public int Id { get; set; }

        [MaxLength(30)]
        [Required]
        public string FirstName { get; set; }

        [MaxLength(30)]
        [Required]
        public string LastName { get; set; }

        public virtual ICollection<Description> Descriptions { get; set; }
    }
}
