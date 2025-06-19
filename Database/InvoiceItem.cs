namespace Margarita;

public partial class InvoiceItem
{
    public Guid Id { get; set; }

    public Guid InvoiceId { get; set; }

    public Guid MenuId { get; set; }

    public int Amount { get; set; }

    public Menu? Menu { get; set; } = null!;
}
