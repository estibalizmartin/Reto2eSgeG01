namespace Reto2eSgeG01.Core.Models
{
    public class EmployeeViewModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? TitleOfCourtesy { get; set; }
        public DateTime? BirthDate { get; set; }
    }
}
