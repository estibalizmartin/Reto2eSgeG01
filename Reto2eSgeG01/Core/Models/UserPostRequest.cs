namespace Reto2eSgeG01.Core.Models
{
    public class UserPostRequest
    {
        public string FirstName { get; set; } = null!;
        public string Password { get; set; }
        public string? Email { get; set; }
    }
}
