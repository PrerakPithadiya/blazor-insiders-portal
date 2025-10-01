namespace Blazor_In_Insiders.Models
{
    public class UserLogin
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime LoginDate { get; set; } = DateTime.UtcNow;
    }
}
