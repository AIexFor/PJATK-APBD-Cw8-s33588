using Microsoft.EntityFrameworkCore;
using Tutorial8.Entities;
using Tutorial8.DTOs;
using Tutorial8.Exceptions;

namespace Tutorial8.Services;

public class DbService(HospitalContext ctx) : IDbService
{
    public async Task<List<PatientGetDto>> GetPatientsAsync(string? search, CancellationToken ct)
    {
        var query = ctx.Patients.AsQueryable();

        if (!string.IsNullOrWhiteSpace(search))
        {
            query = query.Where(p => 
                EF.Functions.Like(p.FirstName, $"%{search}%") || 
                EF.Functions.Like(p.LastName, $"%{search}%"));
        }

        return await query.Select(p => new PatientGetDto(
            p.Pesel,
            p.FirstName,
            p.LastName,
            p.Age,
            p.Sex ? "Male" : "Female",
            p.Admissions.Select(a => new AdmissionDto(
                a.Id, a.AdmissionDate, a.DischargeDate, 
                new WardDto(a.Ward.Id, a.Ward.Name, a.Ward.Description)
            )).ToList(),
            p.BedAssignments.Select(ba => new BedAssignmentDto(
                ba.Id, ba.From, ba.To,
                new BedDto(
                    ba.Bed.Id,
                    new BedTypeDto(ba.Bed.BedType.Id, ba.Bed.BedType.Name, ba.Bed.BedType.Description),
                    new RoomDto(
                        ba.Bed.Room.Id, ba.Bed.Room.HasTv,
                        new WardDto(ba.Bed.Room.Ward.Id, ba.Bed.Room.Ward.Name, ba.Bed.Room.Ward.Description)
                    )
                )
            )).ToList()
        )).ToListAsync(ct);
    }
    
    public async Task AssignBedAsync(string pesel, AssignBedRequestDto request, CancellationToken ct)
    {
        var patientExists = await ctx.Patients.AnyAsync(p => p.Pesel == pesel, ct);
        if (!patientExists) throw new NotFoundException($"Patient with PESEL '{pesel}' not found.");

        var wardExists = await ctx.Wards.AnyAsync(w => w.Name == request.Ward, ct);
        if (!wardExists) throw new NotFoundException($"Ward '{request.Ward}' not found.");

        var bedTypeExists = await ctx.BedTypes.AnyAsync(bt => bt.Name == request.BedType, ct);
        if (!bedTypeExists) throw new NotFoundException($"BedType '{request.BedType}' not found.");
        
        DateTime safeReqTo = request.To ?? new DateTime(2999, 12, 31);

        var availableBed = await ctx.Beds
            .Where(b => b.Room.Ward.Name == request.Ward && b.BedType.Name == request.BedType)
            .Where(b => !b.BedAssignments.Any(a =>
                a.From < safeReqTo && 
                (a.To == null || a.To > request.From))) 
            .FirstOrDefaultAsync(ct);

        if (availableBed == null)
            throw new NotFoundException($"No available beds of type '{request.BedType}' in ward '{request.Ward}' for the requested period.");

        var assignment = new BedAssignment
        {
            PatientPesel = pesel,
            BedId = availableBed.Id,
            From = request.From,
            To = request.To
        };

        ctx.BedAssignments.Add(assignment);
        await ctx.SaveChangesAsync(ct);
    }
}