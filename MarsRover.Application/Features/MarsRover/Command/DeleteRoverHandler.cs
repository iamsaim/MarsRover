using MarsRover.Application.Common.ViewModel;
using MarsRover.Domain.Entities;
using MarsRover.Persistance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MarsRover.Application.Features.MarsRover.Command
{
    public class DeleteRoverCommand : IRequest<bool>
    {
        public Guid data { get; set; }
    }
    public class DeleteRoverHandler : IRequestHandler<DeleteRoverCommand, bool>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DeleteRoverHandler> _logger;

        public DeleteRoverHandler(ApplicationDbContext context, ILogger<DeleteRoverHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteRoverCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Deleting rover");

            var rover = await _context.rovers.Where(x => x.Id == request.data).FirstOrDefaultAsync();

            if(rover == null) return false;

            rover.DeletedDate = DateTime.UtcNow;
            _context.rovers.Update(rover);

            await _context.SaveChangesAsync(cancellationToken);

            return true;

        }
    }
}
