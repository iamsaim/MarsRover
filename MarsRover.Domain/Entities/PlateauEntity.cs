using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Domain.Entities
{
    [Table("Plateau")]
    public class PlateauEntity : BaseEntity
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public virtual ICollection<RoverEntity> Rovers { get; set; }

    }
}
