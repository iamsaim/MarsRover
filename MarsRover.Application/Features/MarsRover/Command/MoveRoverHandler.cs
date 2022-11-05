using MarsRover.Application.Common.ViewModel;
using MarsRover.Domain.Constants;
using MarsRover.Domain.Entities;
using MarsRover.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarsRover.Application.Features.MarsRover.Command
{
    public class MoveRoverCommand : IRequest<bool>
    {
        public MoveRoverVM data { get; set; }
    }
    public class MoveRoverHandler : IRequestHandler<MoveRoverCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MoveRoverHandler> _logger;

        public MoveRoverHandler(ApplicationDbContext context, ILogger<MoveRoverHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(MoveRoverCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Moving rover");

            var rover = await _context.rovers.Where(x => x.Id == request.data.Id).FirstOrDefaultAsync();
            if (rover == null) return false;

            var plateau = await _context.plateaus.Where(x => x.Id == rover.PlateauId).FirstOrDefaultAsync();
            if (plateau == null) return false;


            foreach (char c in request.data.Moves)
            {
                if (c.ToString().Equals("M", StringComparison.InvariantCultureIgnoreCase))
                {
                    switch (rover.CardinalPoint)
                    {
                        case CardinalPoint.N:
                            if(rover.y < plateau.Height)
                                rover.y++;
                            break;

                        case CardinalPoint.S:
                            if(rover.y > 1)
                                rover.y--;
                            break;
                        case CardinalPoint.W:
                            if (rover.x > 1)
                                rover.x--;
                            break;

                        case CardinalPoint.E:
                            if (rover.x < plateau.Width)
                                rover.x++;
                            break;

                        default:
                            break;
                    }

                }

                if (c.ToString().Equals("L", StringComparison.InvariantCultureIgnoreCase))
                {
                    switch (rover.CardinalPoint)
                    {
                        case CardinalPoint.N:
                            rover.CardinalPoint = CardinalPoint.W;
                            break;

                        case CardinalPoint.S:
                            rover.CardinalPoint = CardinalPoint.E;
                            break;
                        case CardinalPoint.W:
                            rover.CardinalPoint = CardinalPoint.S;
                            break;

                        case CardinalPoint.E:
                            rover.CardinalPoint = CardinalPoint.N;
                            break;

                        default:
                            break;
                    }

                }

                if (c.ToString().Equals("R", StringComparison.InvariantCultureIgnoreCase))
                {
                    switch (rover.CardinalPoint)
                    {
                        case CardinalPoint.N:
                            rover.CardinalPoint = CardinalPoint.E;
                            break;

                        case CardinalPoint.S:
                            rover.CardinalPoint = CardinalPoint.W;
                            break;
                        case CardinalPoint.W:
                            rover.CardinalPoint = CardinalPoint.N;
                            break;

                        case CardinalPoint.E:
                            rover.CardinalPoint = CardinalPoint.S;
                            break;

                        default:
                            break;
                    }

                }
            }

            _context.rovers.Update(rover);
            await _context.SaveChangesAsync(cancellationToken);

            return true;

        }
    }
}
