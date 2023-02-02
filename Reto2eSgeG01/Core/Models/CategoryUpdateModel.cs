namespace Reto2eSgeG01.Core.Models
{
    public class CategoryUpdateModel
    {
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
        public byte[]? Picture { get; set; }
    }
}
