namespace Margarita;

public partial class Menu
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Category { get; set; } = null!;

    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? DeletedBy { get; set; }

    public Staff CreatedByNavigation { get; set; } = null!;

    public Staff? DeletedByNavigation { get; set; }
}
