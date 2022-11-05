using MarsRover.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Domain.Entities
{
    [Table("Rover")]
    public class RoverEntity : BaseEntity
    {
        public Guid PlateauId { get; set; }
        public string Name { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public CardinalPoint CardinalPoint { get; set; }

    }
}
