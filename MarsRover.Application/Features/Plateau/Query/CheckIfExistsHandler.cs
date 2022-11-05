using MarsRover.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsRover.Application.Features.Plateau.Query
{
    public class CheckIfExistsQuery : IRequest<bool>
    {

    }
    public class CheckIfExistsHandler : IRequestHandler<CheckIfExistsQuery, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<CheckIfExistsHandler> _logger;

        public CheckIfExistsHandler(ApplicationDbContext context, ILogger<CheckIfExistsHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(CheckIfExistsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Checking count of Plateau");

            var Count =  _context.plateaus.Count();

            return Count > 0 ? true : false;

        }
    }
}
