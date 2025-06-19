namespace Margarita;

public partial class User
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public Guid Password { get; set; }

    public string? Email { get; set; }

    public Guid TeamId { get; set; }

    public string? Pic { get; set; }

    public bool IsRegistered { get; set; }

    public Guid? RegisteredBy { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public Staff CreatedByNavigation { get; set; } = null!;

    public ICollection<Debit> Debits { get; set; } = new List<Debit>();

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public ICollection<Order> OrderCreatedByNavigations { get; set; } = new List<Order>();

    public ICollection<Order> OrderCustomers { get; set; } = new List<Order>();

    public Staff? RegisteredByNavigation { get; set; }

    public Team Team { get; set; } = null!;
}
