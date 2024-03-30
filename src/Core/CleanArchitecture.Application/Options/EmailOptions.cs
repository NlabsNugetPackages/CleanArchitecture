namespace CleanArchitecture.Application.Options;
public sealed class EmailOptions
{
    public string SMTP { get; set; } = "smtp.google.com";
    public string Email { get; set; } = "test@gmail.com";
    public string Password { get; set; } = "testPassword";
    public int PORT { get; set; } = 587;
    public bool SSL { get; set; } = true;
    public bool HTML { get; set; } = true;
}
