using System.Text.Json.Serialization;

namespace Tutorial8.DTOs;

public record BedDto(
    [property: JsonPropertyName("id")] int Id, 
    [property: JsonPropertyName("bedType")] BedTypeDto BedType, 
    [property: JsonPropertyName("room")] RoomDto Room
);