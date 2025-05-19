using System.Text.Json.Serialization;

namespace ApiServices.DTOs.Users;

public class User
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; } = string.Empty;

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = string.Empty;

    [JsonPropertyName("patronomyc")]
    public string Patronomyc { get; set; } = string.Empty;

    [JsonPropertyName("login")]
    public string Login { get; set; } = string.Empty;

    [JsonPropertyName("password")]
    public string Password { get; set; } = string.Empty;

    [JsonPropertyName("pasportSeria")]
    public string PasportSeria { get; set; } = string.Empty;

    [JsonPropertyName("dateOfIssue")]
    public DateTimeOffset DateOfIssue { get; set; }  // pasport berilgan sana
    
    [JsonPropertyName("dateOfExpiry")]
    public DateTimeOffset DateOfExpiry { get; set; } // Amal qilish muddati
    
    [JsonPropertyName("address")]
    public string Address { get; set; } = string.Empty;
    
    [JsonPropertyName("jShShIR")]
    public string JShShIR { get; set; } = string.Empty;

    [JsonPropertyName("phone")]
    public string Phone { get; set; } = string.Empty;

    [JsonPropertyName("dateOfBirth")]
    public DateTimeOffset DateOfBirth { get; set; }

    [JsonPropertyName("gender")]
    public int Gender { get; set; } // Enum o‘rniga int, chunki JSON’da raqam

    [JsonPropertyName("roleId")]
    public long RoleId { get; set; }

    [JsonPropertyName("photoId")]
    public long? PhotoId { get; set; }

    [JsonPropertyName("createdBy")]
    public long CreatedBy { get; set; }

    [JsonPropertyName("createdAt")]
    public DateTimeOffset CreatedAt { get; set; }

    [JsonPropertyName("updatedBy")]
    public long UpdatedBy { get; set; }

    [JsonPropertyName("updatedAt")]
    public DateTimeOffset UpdatedAt { get; set; }
}