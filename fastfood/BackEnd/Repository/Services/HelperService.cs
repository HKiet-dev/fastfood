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
            if (length < 4)
            {
                throw new ArgumentException("Password length must be at least 4 characters.");
            }

            const string upperChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string lowerChars = "abcdefghijklmnopqrstuvwxyz";
            const string digitChars = "0123456789";
            const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

            char[] password = new char[length];

            // Lấy 1 ký tự ngẫu nhiên từ mỗi loại ký tự
            password[0] = upperChars[random.Next(upperChars.Length)];
            password[1] = lowerChars[random.Next(lowerChars.Length)];
            password[2] = digitChars[random.Next(digitChars.Length)];
            password[3] = specialChars[random.Next(specialChars.Length)];

            // Lấy các ký tự còn lại
            string allChars = upperChars + lowerChars + digitChars + specialChars;
            for (int i = 4; i < length; i++)
            {
                password[i] = allChars[random.Next(allChars.Length)];
            }

            // Trộn mảng ký tự
            return new string(password.OrderBy(x => random.Next()).ToArray());
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
