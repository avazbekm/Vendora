using System.Text.Json.Serialization;

namespace ApiServices.DTOs.Roles;

public class Role
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("roleName")]
    public string RoleName { get; set; } = string.Empty;
    
    [JsonPropertyName("createdBy")]
    public long CreatedBy { get; set; }
    
    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("updatedBy")]
    public long? UpdatedBy { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTimeOffset? UpdatedAt { get; set; }

}
