namespace GarageLog.Models
{
    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; } = string.Empty;
        public byte[] PasswordHash { get; set; } = { 0 };
        public byte[] PasswordSalt { get; set; } = { 0 };
    }
}
