using System.Net;
using System.Net.Mail;
using Application.Common.Helpers;
using Application.Common.Interfaces;
using Application.Common.Models.Email;

namespace Infrastructure.Services;

public class EmailManager:IEmailService
{
    private readonly string _wwwrootPath;
    public EmailManager(string wwwrootPath)
    {
        _wwwrootPath = wwwrootPath;
    }

    //hata maili gönderecek metodlar
    public void SendEmailConfirmation(SendEmailConfirmationDto sendEmailConfirmationDto)
    {
        var htmlContent = File.ReadAllText($"{_wwwrootPath}/email_templates/email_confirmation.html");
        
        htmlContent = htmlContent.Replace("{{subject}}", MessagesHelper.Email.Confirmation.Subject);
        
        //htmlContent = htmlContent.Replace("{{name}}", MessagesHelper.Email.Confirmation.Name(sendEmailConfirmationDto.Name));
        
        htmlContent = htmlContent.Replace("{{message}}", MessagesHelper.Email.Confirmation.Message(sendEmailConfirmationDto.Message));

        //htmlContent = htmlContent.Replace("{{buttonText}}", MessagesHelper.Email.Confirmation.ButtonText);
        
        //htmlContent = htmlContent.Replace("{{buttonLink}}", MessagesHelper.Email.Confirmation.Buttonlink(sendEmailConfirmationDto.Email,sendEmailConfirmationDto.Token));

        Send(new SendEmailDto(sendEmailConfirmationDto.Email,htmlContent,MessagesHelper.Email.Confirmation.Subject));

    }

    private void Send(SendEmailDto sendEmailDto)
    {
        MailMessage message = new MailMessage();

        message.To.Add(sendEmailDto.EmailAddress);
        message.From = new MailAddress("noreply@entegraturk.com");

        message.Subject = sendEmailDto.Subject;

        message.IsBodyHtml = true;

        message.Body = sendEmailDto.Content;

        SmtpClient client = new SmtpClient();

        client.Port = 587;

        client.Host = "mail.entegraturk.com";

        client.EnableSsl = false;

        client.UseDefaultCredentials = false;

        client.Credentials = new NetworkCredential("noreply@entegraturk.com", "xzx2xg4Jttrbzm5nIJ2kj1pE4l");

        client.DeliveryMethod = SmtpDeliveryMethod.Network;
        
        client.Send(message);


    }
}