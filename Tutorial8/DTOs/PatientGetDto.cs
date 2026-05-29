using System.Text.Json.Serialization;

namespace Tutorial8.DTOs;

public record PatientGetDto(
    [property: JsonPropertyName("pesel")] string Pesel, 
    [property: JsonPropertyName("firstName")] string FirstName, 
    [property: JsonPropertyName("lastName")] string LastName, 
    [property: JsonPropertyName("age")] int Age, 
    [property: JsonPropertyName("sex")] string Sex, 
    [property: JsonPropertyName("admissions")] List<AdmissionDto> Admissions, 
    [property: JsonPropertyName("bedAssignments")] List<BedAssignmentDto> BedAssignments
);