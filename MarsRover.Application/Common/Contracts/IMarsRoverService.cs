using MarsRover.Application.Common.ViewModel;
using MarsRover.Application.Common.ViewModel.Response;

namespace MarsRover.Application.Common.Contracts;

public interface IMarsRoverService
{
    public Task<Guid> DeployRovers(DeployRoverVM request, CancellationToken token);
    public Task<List<GetRoversVM>> GetRovers(Guid PlateauId, CancellationToken token);
    public Task<bool> MoveRover(MoveRoverVM request, CancellationToken token);
    public Task<bool> DeleteRover(Guid Id, CancellationToken token);
}