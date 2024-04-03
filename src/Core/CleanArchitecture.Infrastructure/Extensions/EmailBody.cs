namespace CleanArchitecture.Infrastructure.Extensions;
public static class EmailBody
{
    public static string CreateConfirmEmailBody(string emailConfirmCode, int minute)
    {
        var body = @"
                        <!DOCTYPE html>
                        <html lang=""en"">
                        <head>
                            <meta charset=""UTF-8"">
                            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                            <title>Email Confirmation Code</title>
                            <style>
                                /* Stil özellikleri */
                                body {
                                    font-family: Arial, sans-serif;
                                    background-color: #f4f4f4;
                                    padding: 20px;
                                }
                                .container {
                                    max-width: 600px;
                                    margin: 0 auto;
                                    background-color: #fff;
                                    border-radius: 10px;
                                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                                    padding: 20px;
                                    text-align: center;
                                    justify-content: center;
                                    align-items: center;
                                }
                                .confirmation-code {
                                    display: flex;
                                    justify-content: center;
                                    align-items: center;
                                    margin-top: 20px;
                                    margin-left: 50px;
                                }
                                .digit-container {
                                    display: flex;
                                    width: auto; /* Kutu genişliğini artır */
                                    height: auto;
                                    border: 2px solid #007bff;
                                    border-radius: 10px;
                                    margin-right: 10px;
                                    font-size: 55px;
                                    font-weight: bold;
                                    color: #007bff;
                                    text-align: center;
                                    inherit: text-align;
                                }
                            </style>
                        </head>
                        <body>
                            <div class=""container"">
                                <h2 style=""color: #007bff;"">Email Confirmation Code</h2>
                                <p>Please use the following code to confirm your email:</p>
                                <div class=""confirmation-code"">
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[0] + @" </div></div>
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[1] + @" </div></div>
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[2] + @" </div></div>
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[3] + @" </div></div>
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[4] + @" </div></div>
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[5] + @" </div></div>
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[6] + @" </div></div>
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[7] + @" </div></div>
                                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + emailConfirmCode[8] + @" </div></div>
                                </div>
                                <p style=""margin-top: 20px;"">This code will expire in " + minute + @" minutes.</p>
                            </div>
                        </body>
                        </html>
                        ";

        return body;
    }

    public static string CreateSendForgotPasswordCodeEmailBody(string forgotPasswordCode, int minute)
    {
        string body = @"
        <!DOCTYPE html>
        <html lang=""en"">
        <head>
            <meta charset=""UTF-8"">
            <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            <title>Email Confirmation Code</title>
            <style>
                /* Stil özellikleri */
                body {
                    font-family: Arial, sans-serif;
                    background-color: #f4f4f4;
                    padding: 20px;
                }
                .container {
                    max-width: 600px;
                    margin: 0 auto;
                    background-color: #fff;
                    border-radius: 10px;
                    box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
                    padding: 20px;
                    text-align: center;
                    justify-content: center;
                    align-items: center;
                }
                .confirmation-code {
                    display: flex;
                    justify-content: center;
                    align-items: center;
                    margin-top: 20px;
                    margin-left: 50px;
                }
                .digit-container {
                    display: flex;
                    width: auto; /* Kutu genişliğini artır */
                    height: auto;
                    border: 2px solid #007bff;
                    border-radius: 10px;
                    margin-right: 10px;
                    font-size: 55px;
                    font-weight: bold;
                    color: #007bff;
                    text-align: center;
                    inherit: text-align;
                }
            </style>
        </head>
        <body>
            <div class=""container"">
                <h2 style=""color: #007bff;"">Reset Your Password</h2>
                <p>Please use the following code to reset your password:</p>
                <div class=""confirmation-code"">
                    <!-- Her bir rakam için ayrı bir kutu oluşturuluyor -->
                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + forgotPasswordCode[0] + @" </div></div>
                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + forgotPasswordCode[1] + @" </div></div>
                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + forgotPasswordCode[2] + @" </div></div>
                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + forgotPasswordCode[3] + @" </div></div>
                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + forgotPasswordCode[4] + @" </div></div>
                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + forgotPasswordCode[5] + @" </div></div>
                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + forgotPasswordCode[6] + @" </div></div>
                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + forgotPasswordCode[7] + @" </div></div>
                    <div class=""digit-container""> <div style=""padding-right: 20px; padding-left: 20px; ""> " + forgotPasswordCode[8] + @" </div></div>
                </div>
                <p style=""margin-top: 20px;"">This code will expire in " + minute + @" minutes.</p>
            </div>
        </body>
        </html>
        ";

        return body;
    }

    public static string MaskEmail(string email)
    {
        var atIndex = email.IndexOf('@');
        if (atIndex == -1) return email;

        var username = email.Substring(0, atIndex);
        var domain = email.Substring(atIndex + 1);

        var maskedUsername = username.Length > 1
            ? username[0] + new string('*', username.Length - 2) + username[^1]
            : username;

        var domainParts = domain.Split('.');
        if (domainParts.Length > 1)
        {
            var domainName = domainParts[0];
            var maskedDomainName = domainName.Length > 2
                ? domainName.Substring(0, 2) + new string('*', domainName.Length - 2)
                : domainName;

            var maskedDomain = maskedDomainName + "." + string.Join(".", domainParts[1..]);
            return maskedUsername + "@" + maskedDomain;
        }

        return maskedUsername + "@" + domain;
    }
}
