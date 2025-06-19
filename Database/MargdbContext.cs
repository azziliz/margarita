using Microsoft.EntityFrameworkCore;

namespace Margarita;

public partial class MargdbContext : DbContext
{
    public MargdbContext()
    {
    }

    public MargdbContext(DbContextOptions<MargdbContext> options)
        : base(options)
    {
    }

    public DbSet<Debit> Debits { get; set; }

    public DbSet<Invoice> Invoices { get; set; }

    public DbSet<InvoiceItem> InvoiceItems { get; set; }

    public DbSet<Menu> Menus { get; set; }

    public DbSet<Order> Orders { get; set; }

    public DbSet<OrderItem> OrderItems { get; set; }

    public DbSet<Staff> Staff { get; set; }

    public DbSet<Team> Teams { get; set; }

    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=192.168.1.1;Initial Catalog=margdb;User ID=sa;Password=CK149pce+;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Debit>(entity =>
        {
            entity.ToTable("Debit", "Margarita");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())").HasConversion(
                v => v,
                v => new DateTime(v.Ticks, DateTimeKind.Utc));
            entity.Property(e => e.Total).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany()
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Debit_Creator");

            entity.HasOne(d => d.Customer).WithMany(p => p.Debits)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Debit_Customer");
        });

        modelBuilder.Entity<Invoice>(entity =>
        {
            entity.ToTable("Invoice", "Margarita");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())").HasConversion(
                v => v,
                v => new DateTime(v.Ticks, DateTimeKind.Utc));
            entity.Property(e => e.Total).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany()
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Creator");

            entity.HasOne(d => d.Customer).WithMany(p => p.Invoices)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Customer");

            entity.HasMany(p => p.InvoiceItems).WithOne()
               .HasForeignKey(d => d.InvoiceId)
               .OnDelete(DeleteBehavior.ClientSetNull)
               .HasConstraintName("FK_InvoiceItem_Invoice");

            entity.HasOne(d => d.Order).WithMany()
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Invoice_Order");
        });

        modelBuilder.Entity<InvoiceItem>(entity =>
        {
            entity.ToTable("InvoiceItem", "Margarita");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Menu).WithMany()
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_InvoiceItem_Menu");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.ToTable("Menu", "Margarita");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())").HasConversion(
                v => v,
                v => new DateTime(v.Ticks, DateTimeKind.Utc));
            entity.Property(e => e.Price).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.CreatedByNavigation).WithMany()
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Menu_Creator");

            entity.HasOne(d => d.DeletedByNavigation).WithMany()
                .HasForeignKey(d => d.DeletedBy)
                .HasConstraintName("FK_Menu_Destructor");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.ToTable("Order", "Margarita");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())").HasConversion(
                v => v,
                v => new DateTime(v.Ticks, DateTimeKind.Utc));

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OrderCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Creator");

            entity.HasOne(d => d.Customer).WithMany(p => p.OrderCustomers)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Order_Customer");

            entity.HasOne(d => d.TakenInChargeByNavigation).WithMany()
                .HasForeignKey(d => d.TakenInChargeBy)
                .HasConstraintName("FK_Order_TakenInCharge");

            entity.HasMany(p => p.OrderItems).WithOne()
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Order");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.ToTable("OrderItem", "Margarita");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

            entity.HasOne(d => d.Menu).WithMany()
                .HasForeignKey(d => d.MenuId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderItem_Menu");
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.ToTable("Staff", "Margarita");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())").HasConversion(
                v => v,
                v => new DateTime(v.Ticks, DateTimeKind.Utc));

            entity.HasOne(d => d.CreatedByNavigation).WithMany()
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Staff_Creator");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.ToTable("Team", "Margarita");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())").HasConversion(
                v => v,
                v => new DateTime(v.Ticks, DateTimeKind.Utc));

            entity.HasOne(d => d.CreatedByNavigation).WithMany()
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Team_Creator");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User", "Margarita");

            entity.Property(e => e.Id).HasDefaultValueSql("(newid())");
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getutcdate())").HasConversion(
                v => v,
                v => new DateTime(v.Ticks, DateTimeKind.Utc));
            entity.Property(e => e.Pic).IsUnicode(false);

            entity.HasOne(d => d.CreatedByNavigation).WithMany()
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Creator");

            entity.HasOne(d => d.RegisteredByNavigation).WithMany()
                .HasForeignKey(d => d.RegisteredBy)
                .HasConstraintName("FK_User_Registerer");

            entity.HasOne(d => d.Team).WithMany()
                .HasForeignKey(d => d.TeamId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Team");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
