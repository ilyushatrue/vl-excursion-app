using DAL.Models.Base;

namespace DAL.Models;

public class User : BaseEntity
{
    public string Name { get; set; } = null!;
    public string? LastName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
}
