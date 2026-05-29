using System.Text.Json.Serialization;

namespace Tutorial8.DTOs;

public record AdmissionDto(
    [property: JsonPropertyName("id")] int Id, 
    [property: JsonPropertyName("admissionDate")] DateTime AdmissionDate, 
    [property: JsonPropertyName("dischargeDate")] DateTime? DischargeDate, 
    [property: JsonPropertyName("ward")] WardDto Ward
);