namespace MovieStoreApi.Service.Email;

public class EmailOptions
{
    public const string SectionName = "EmailSettings";

    public int Port { get; set; }
    public string From { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Host { get; set; } = string.Empty;
    public bool EnableSsl { get; set; } 
}