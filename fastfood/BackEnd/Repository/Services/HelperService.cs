using BackEnd.Repository.Interfaces;
using System.Net.Mail;
using System.Net;

namespace BackEnd.Repository.Services
{
    public class HelperService : IHelper
    {
        public string GenerateRandomPassword(int length = 8)
        {
            Random random = new Random();
            string password = "";
            for (int i = 0; i < length; i++)
            {
                password += (char)random.Next(33, 126);
            }
            return password;
        }

        public async Task SendMail(string email, string subject, string content)
        {
            string bodyHtml = $@"
                <p style=""font-size: 18px;"">
                    {subject}
                    <br>
                    Đây là mật khẩu mới của bạn
                    <br>
                </p>
    
                <p style=""border: 2px ;background-color: rgb(0, 101, 209);font-size: 40px; color: white; letter-spacing: 5px; text-align: center; width: auto;"">
                        {content}
                </p>";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("kiet43012@gmail.com", "fjrq yuus fmaf ugbt"),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("kiet43012@gmail.com"),
                Subject = subject,
                Body = bodyHtml,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
