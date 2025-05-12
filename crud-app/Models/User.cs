namespace crud_app.Models
{
    public class User
    {
        public int Id { get; set; } // Primary Key
        public string Username { get; set; }
        public string PasswordHash { get; set; } // Store hashed passwords for security
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}