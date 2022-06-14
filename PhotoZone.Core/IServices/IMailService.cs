namespace PhotoZone.Core.IServices;

public interface IMailService
{
   void sendMailRegistration(string email, string name);

   void sendFeedback(string email, string name, string text);

   void sendMailChangePassword(string email, string name);

   void sendMailChangeMail(string email, string newEmail, string name);
}