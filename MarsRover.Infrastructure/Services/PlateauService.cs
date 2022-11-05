using MarsRover.Application.Common.Contracts;
using MarsRover.Application.Common.ViewModel;
using MarsRover.Application.Features.Plateau.Command;
using MarsRover.Application.Features.Plateau.Query;
using MediatR;

namespace MarsRover.Infrastructure.Services
{
    public class PlateauService : IPlateauService
    {
        private readonly IMediator _mediator;
        public PlateauService(IMediator mediator) => _mediator = mediator;

        public Task<Guid> InitiatePlateau(InitiatePlateauVM request, CancellationToken token) => _mediator.Send(new InitiatePlateauCommand { data = request},token);
        public Task<bool> CheckIfExists(CancellationToken token) => _mediator.Send(new CheckIfExistsQuery { },token);
        public Task<GetPlateauVM> GetPlateau(Guid Id,CancellationToken token) => _mediator.Send(new GetPlateauQuery { PlateauId = Id},token);

    }
}
