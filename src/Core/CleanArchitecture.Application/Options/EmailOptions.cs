namespace CleanArchitecture.Application.Options;
public sealed class EmailOptions
{
    public string SMTP { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public int PORT { get; set; } = 587;
    public bool SSL { get; set; } = true;
    public bool HTML { get; set; } = true;


}
