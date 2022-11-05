using MarsRover.Application.Common.ViewModel;
using MarsRover.Domain.Entities;
using MarsRover.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarsRover.Application.Features.Plateau.Query
{
    public class GetPlateauQuery : IRequest<GetPlateauVM>
    {
        public Guid PlateauId { get; set; }
    }
    public class GetPlateauHandler : IRequestHandler<GetPlateauQuery, GetPlateauVM>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GetPlateauHandler> _logger;

        public GetPlateauHandler(ApplicationDbContext context, ILogger<GetPlateauHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<GetPlateauVM> Handle(GetPlateauQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting Plateau with plateauId: {request.PlateauId}");

            var plateauEntity = _context.plateaus.Where(x => x.Id == request.PlateauId).FirstOrDefault();
            var rovers = _context.rovers.Where(x => x.PlateauId == request.PlateauId && x.DeletedDate == null);

            var plateau = new GetPlateauVM
            {
                Id = plateauEntity.Id,
                Width = plateauEntity.Width,
                Height = plateauEntity.Height
            };
            plateau.rovers = rovers.Select(x => new GetRoversVM
            {
                Id = x.Id,
                PlateauId = x.PlateauId,
                x = x.x,
                y = x.y,
                Name = x.Name,
                CardinalPoint = x.CardinalPoint
            }).ToList();


            return plateau;

        }
    }
}
