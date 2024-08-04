namespace BackEnd.Models.Dtos
{
    public class ChangePassDto
    {
        public string id { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
    }
}
