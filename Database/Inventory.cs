namespace Margarita;

public partial class Inventory
{
    public Guid Id { get; set; }

    public bool IsActive { get; set; }

    public Guid ContainerId { get; set; }

    public decimal Number { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? DeletedBy { get; set; }

    public Container? Container { get; set; } = null!;

    public Staff? CreatedByNavigation { get; set; } = null!;

    public Staff? DeletedByNavigation { get; set; }
}
