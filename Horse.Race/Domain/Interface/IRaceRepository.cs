using Domain.Models;
using Domain.Models.API.Entities;


namespace Domain.Interface;

public interface IRaceRepository
{

    /// <summary>
    /// Get races.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Race list</returns>
    Task<IEnumerable<RaceUpdateDto>?> GetRacesAsync(CancellationToken cancellationToken = default);

}