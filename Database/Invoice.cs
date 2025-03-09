namespace Margarita;

public partial class Invoice
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid CustomerId { get; set; }

    public decimal Total { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public Staff CreatedByNavigation { get; set; } = null!;

    public User Customer { get; set; } = null!;

    public ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

    public Order Order { get; set; } = null!;
}
