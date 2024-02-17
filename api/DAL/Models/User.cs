namespace DAL.Models;

public class User
{
    private string? _phone;

    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Email { get; set; }

    public string? Phone
    {
        get => _phone;
        set => _phone = value?.Length == 11 ? value : null;
    }
}
