using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MoviesSystem.Models
{
    public class Description
    {
        public Description()
        {
            this.Actors = new HashSet<Actor>();
        }

        [ForeignKey("Movie")]
        public int Id { get; set; }

        [Column(TypeName = "text")]
        public string Summary { get; set; }

        public DateTime Year { get; set; }

        public virtual Movie Movie { get; set; }

        public virtual ICollection<Actor> Actors{ get; set; }
    }
}
