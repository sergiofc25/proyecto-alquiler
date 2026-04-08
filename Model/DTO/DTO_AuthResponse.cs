namespace Model.DTO;

public class DTO_AuthResponse
{
    public bool IsAuthSuccessful { get; set; }

    public string? ErrorMessage { get; set; }

    public string? Token { get; set; }

    public string? RefreshToken { get; set; }
    public DateTimeOffset Expires { get; set; }
}