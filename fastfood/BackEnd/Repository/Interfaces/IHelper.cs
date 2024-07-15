namespace BackEnd.Repository.Interfaces
{
    public interface IHelper
    {
        Task SendMail(string email, string subject, string content);
        string GenerateRandomPassword(int length = 8);
    }
}
