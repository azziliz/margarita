namespace Margarita;

public partial class Session
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public bool IsCurrent { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public Staff? CreatedByNavigation { get; set; } = null!;
}
