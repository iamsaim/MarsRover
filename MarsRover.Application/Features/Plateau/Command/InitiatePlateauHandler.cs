using MarsRover.Application.Common.ViewModel;
using MarsRover.Domain.Entities;
using MarsRover.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarsRover.Application.Features.Plateau.Command
{
    public class InitiatePlateauCommand : IRequest<Guid> 
    {
        public InitiatePlateauVM data { get; set; }
    }
    public class InitiatePlateauHandler : IRequestHandler<InitiatePlateauCommand, Guid>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<InitiatePlateauHandler> _logger;

        public InitiatePlateauHandler(ApplicationDbContext context, ILogger<InitiatePlateauHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Guid> Handle(InitiatePlateauCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Inserting Plateau");

            var plateau = new PlateauEntity()
            {
                Width = request.data.Width,
                Height = request.data.Height
            };

            var isAdded = await _context.plateaus.AddAsync(plateau, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return isAdded.State == EntityState.Unchanged?plateau.Id : Guid.Empty;

        }
    }
}
