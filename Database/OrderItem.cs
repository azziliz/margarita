namespace Margarita;

public partial class OrderItem
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid MenuId { get; set; }

    public int Amount { get; set; }

    public Menu? Menu { get; set; } = null!;
}
