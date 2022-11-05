using MarsRover.Application.Common.Contracts;
using MarsRover.Application.Common.ViewModel;
using MarsRover.Application.Features.MarsRover.Command;
using MarsRover.Application.Features.MarsRover.Query;
using MediatR;

namespace MarsRover.Infrastructure.Services
{
    public class MarsRoverService : IMarsRoverService
    {
        private readonly IMediator _mediator;
        public MarsRoverService(IMediator mediator) => _mediator = mediator;

        public Task<Guid> DeployRovers(DeployRoverVM request, CancellationToken token) => _mediator.Send(new DeployRoversCommand { data = request }, token);
        public Task<List<GetRoversVM>> GetRovers(Guid PlateauId,CancellationToken token) => _mediator.Send(new GetRoversQuery { PlateauId = PlateauId }, token);
        public Task<bool> MoveRover(MoveRoverVM request,CancellationToken token) => _mediator.Send(new MoveRoverCommand { data = request }, token);
        public Task<bool> DeleteRover(Guid Id,CancellationToken token) => _mediator.Send(new DeleteRoverCommand { data = Id }, token);
    }
}
