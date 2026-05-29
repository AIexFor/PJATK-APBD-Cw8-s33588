using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Tutorial8.DTOs;

public record AssignBedRequestDto(
    [Required] 
    [property: JsonPropertyName("from")] DateTime From, 
    
    [property: JsonPropertyName("to")] DateTime? To, 
    
    [Required] 
    [property: JsonPropertyName("bedType")] string BedType, 
    
    [Required] 
    [property: JsonPropertyName("ward")] string Ward
);