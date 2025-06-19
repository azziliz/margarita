namespace Margarita;

public partial class Ingredient
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public Guid? DeletedBy { get; set; }

    public Staff? CreatedByNavigation { get; set; } = null!;

    public Staff? DeletedByNavigation { get; set; }

    public ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
