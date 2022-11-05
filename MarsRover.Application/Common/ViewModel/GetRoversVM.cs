using MarsRover.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Application.Common.ViewModel
{
    public class GetRoversVM
    {
        public Guid Id { get; set; }
        public Guid PlateauId { get; set; }
        public string Name { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public CardinalPoint CardinalPoint { get; set; }
    }
}
