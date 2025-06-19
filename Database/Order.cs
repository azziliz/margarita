namespace Margarita;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid? CustomerId { get; set; }

    public bool IsDelivered { get; set; }

    public DateTime TakenInChargeDate { get; set; }

    public Guid? TakenInChargeBy { get; set; }

    public DateTime PreparationDate { get; set; }

    public DateTime DeliveryDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public User? CreatedByNavigation { get; set; } = null!;

    public User? Customer { get; set; } = null!;

    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public Staff? TakenInChargeByNavigation { get; set; }
}
