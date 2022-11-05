using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Application.Common.ViewModel
{
    public class GetPlateauVM
    {
        public Guid Id { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public List<GetRoversVM> rovers { get; set; }
    }
}
