namespace Margarita;

public partial class Container
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public Guid IngredientId { get; set; }

    public decimal Quantity { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? DeletedBy { get; set; }

    public Staff CreatedByNavigation { get; set; } = null!;

    public Staff? DeletedByNavigation { get; set; }

    public Ingredient? Ingredient { get; set; } = null!;

    public ICollection<Inventory> Inventories { get; set; } = new List<Inventory>();
}
