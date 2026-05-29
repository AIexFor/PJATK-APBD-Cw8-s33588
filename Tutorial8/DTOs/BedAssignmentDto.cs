using System.Text.Json.Serialization;

namespace Tutorial8.DTOs;

public record BedAssignmentDto(
    [property: JsonPropertyName("id")] int Id, 
    [property: JsonPropertyName("from")] DateTime From, 
    [property: JsonPropertyName("to")] DateTime? To, 
    [property: JsonPropertyName("bed")] BedDto Bed
);