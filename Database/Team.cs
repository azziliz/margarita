namespace Margarita;

public partial class Team
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Email { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public Staff CreatedByNavigation { get; set; } = null!;
}
