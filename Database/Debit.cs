﻿namespace Margarita;

public partial class Debit
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public string Medium { get; set; } = null!;

    public decimal Total { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? CreatedBy { get; set; }

    public Staff? CreatedByNavigation { get; set; } = null!;

    public User? Customer { get; set; } = null!;
}
