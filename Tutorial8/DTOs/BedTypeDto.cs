using System.Text.Json.Serialization;

namespace Tutorial8.DTOs;

public record BedTypeDto(
    [property: JsonPropertyName("id")] int Id, 
    [property: JsonPropertyName("name")] string Name, 
    [property: JsonPropertyName("description")] string Description
);