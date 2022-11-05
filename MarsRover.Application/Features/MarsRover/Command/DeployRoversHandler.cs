using MarsRover.Application.Common.ViewModel;
using MarsRover.Domain.Entities;
using MarsRover.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarsRover.Application.Features.MarsRover.Command
{
    public class DeployRoversCommand : IRequest<Guid>
    {
        public DeployRoverVM data { get; set; }
    }
    public class DeployRoversHandler : IRequestHandler<DeployRoversCommand, Guid>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DeployRoversHandler> _logger;

        public DeployRoversHandler(ApplicationDbContext context, ILogger<DeployRoversHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Guid> Handle(DeployRoversCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Inserting rover");

            var plateau = await _context.plateaus.FirstOrDefaultAsync(x => x.Id == request.data.PlateauId);

            if (plateau == null || request.data.x > plateau.Width || request.data.y > plateau.Height ||
                request.data.x < 1 || request.data.y < 1)
                return Guid.Empty;

            var rover = new RoverEntity()
            {
                PlateauId = request.data.PlateauId,
                Name = request.data.Name,
                x = request.data.x,
                y = request.data.y,
                CardinalPoint = request.data.CardinalPoint
            };

            var isAdded =  await _context.rovers.AddAsync(rover, cancellationToken);



            await _context.SaveChangesAsync(cancellationToken);

            return isAdded.State == EntityState.Unchanged ? rover.Id : Guid.Empty; ;

        }
    }
}
