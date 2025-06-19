namespace Margarita;

public partial class Recipe
{
    public Guid Id { get; set; }

    public bool IsActive { get; set; }

    public Guid MenuId { get; set; }

    public Guid IngredientId { get; set; }

    public decimal Quantity { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid CreatedBy { get; set; }

    public Guid? DeletedBy { get; set; }

    public Staff? CreatedByNavigation { get; set; } = null!;

    public Staff? DeletedByNavigation { get; set; }

    public Ingredient? Ingredient { get; set; } = null!;

    public Menu? Menu { get; set; } = null!;
}
