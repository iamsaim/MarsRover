using MarsRover.Application.Common.ViewModel;
using MarsRover.Domain.Entities;
using MarsRover.Persistance;
using MediatR;
using Microsoft.Extensions.Logging;

namespace MarsRover.Application.Features.MarsRover.Query
{
    public class GetRoversQuery : IRequest<List<GetRoversVM>>
    {
        public Guid PlateauId { get; set; }
    }
    public class GetRoversHandler : IRequestHandler<GetRoversQuery, List<GetRoversVM>>
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<GetRoversHandler> _logger;

        public GetRoversHandler(ApplicationDbContext context, ILogger<GetRoversHandler> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<GetRoversVM>> Handle(GetRoversQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Getting all rovers with plateauId: {request.PlateauId}");

            var rovers =  _context.rovers.Where(x=>x.PlateauId == request.PlateauId && x.DeletedDate == null);

            var response = rovers.Select(x => new GetRoversVM
            { 
                Id = x.Id,
                PlateauId = x.PlateauId,
                x = x.x,
                y = x.y,
                Name = x.Name,
                CardinalPoint = x.CardinalPoint
            }).ToList();
            

            return response;

        }
    }
}
