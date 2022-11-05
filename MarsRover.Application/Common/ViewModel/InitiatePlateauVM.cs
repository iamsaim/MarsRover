using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Application.Common.ViewModel
{
    public class InitiatePlateauVM
    {
        [Required]
        [Range(1, 10)]
        public int Width { get; set; }
        [Required]
        [Range(1, 10)]
        public int Height { get; set; }
    }
}
