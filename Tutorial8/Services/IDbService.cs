using Tutorial8.DTOs;

namespace Tutorial8.Services;

public interface IDbService
{
    Task<List<PatientGetDto>> GetPatientsAsync(string? search, CancellationToken ct);
    Task AssignBedAsync(string pesel, AssignBedRequestDto request, CancellationToken ct);
}