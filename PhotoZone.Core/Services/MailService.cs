using MailKit.Security;
using MimeKit;
using PhotoZone.Core.Exceptions;
using PhotoZone.Core.IServices;

namespace PhotoZone.Services;

public class MailService : IMailService
{
    public void sendMailRegistration(string email, string userName)
    {
        try
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("PhotoZone","lion20914king@gmail.com"));
            message.To.Add(new MailboxAddress(userName, email));
            message.Subject = $"Welcome {userName} to PhotoZone {email}";
            var html = "<img src = 'https://i.ibb.co/ZHWYyhY/people-2572935-1280.png' alt = 'PhotoZone'/>";
            message.Body = new BodyBuilder { HtmlBody =  html}.ToMessageBody();
            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("lion20914king@gmail.com", "vafwlujvtzimuxyj");
                client.Send(message);
                client.Disconnect(true);
            }
        }
        catch (PhotoZoneException)
        {
            throw new PhotoZoneException("Error in message");
        }
    }

    public void sendFeedback(string email, string name, string text)
    {
        try
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress(email,email));
            message.To.Add(new MailboxAddress("Maks", "marianchuk.maksym.clg@chnu.edu.ua"));
            message.Subject = $"Відгук від {name} по адресу {email}";
            message.Body = new BodyBuilder { TextBody = text }.ToMessageBody();
            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("lion20914king@gmail.com", "vafwlujvtzimuxyj");
                client.Send(message);
                client.Disconnect(true);
            }
        }
        catch (PhotoZoneException)
        {
            throw new PhotoZoneException("Error in message");
        }
    }

    public void sendMailChangePassword(string email,string name)
    {
        try
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("PhotoZone","lion20914king@gmail.com"));
            message.To.Add(new MailboxAddress(name, email));
            message.Subject = $"Attention!";
            message.Body = new BodyBuilder { TextBody = "Your password is was changed" }.ToMessageBody();
            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("lion20914king@gmail.com", "vafwlujvtzimuxyj");
                client.Send(message);
                client.Disconnect(true);
            }
        }
        catch (PhotoZoneException)
        {
            throw new PhotoZoneException("Error in message");
        }
    }

    public void sendMailChangeMail(string email, string newEmail, string name)
    {
        try
        {
            MimeMessage message = new MimeMessage();
            message.From.Add(new MailboxAddress("PhotoZone","lion20914king@gmail.com"));
            message.To.Add(new MailboxAddress(name, newEmail));
            message.Subject = $"Attention!";
            message.Body = new BodyBuilder { TextBody = $"Your email was Changed from {email} to {newEmail}" }.ToMessageBody();
            using (MailKit.Net.Smtp.SmtpClient client = new MailKit.Net.Smtp.SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                client.Authenticate("lion20914king@gmail.com", "vafwlujvtzimuxyj");
                client.Send(message);
                client.Disconnect(true);
            }
        }
        catch (PhotoZoneException)
        {
            throw new PhotoZoneException("Error in message");
        }
    }
}