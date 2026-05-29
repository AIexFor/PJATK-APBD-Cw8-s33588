using System.Text.Json.Serialization;

namespace Tutorial8.DTOs;

public record RoomDto(
    [property: JsonPropertyName("id")] string Id, 
    [property: JsonPropertyName("hasTv")] bool HasTv, 
    [property: JsonPropertyName("ward")] WardDto Ward
);