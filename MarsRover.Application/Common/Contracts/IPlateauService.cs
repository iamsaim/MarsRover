using MarsRover.Application.Common.ViewModel;
using MarsRover.Application.Common.ViewModel.Response;

namespace MarsRover.Application.Common.Contracts;

public interface IPlateauService
{
    public Task<Guid> InitiatePlateau(InitiatePlateauVM request, CancellationToken token);
    public Task<bool> CheckIfExists(CancellationToken token);
    public Task<GetPlateauVM> GetPlateau(Guid Id, CancellationToken token);
}