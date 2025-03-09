namespace Margarita;

public partial class Debit
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public decimal Total { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public virtual Staff CreatedByNavigation { get; set; } = null!;

    public virtual User Customer { get; set; } = null!;
}
