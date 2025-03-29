using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ResApi.Models
{
    public partial class DataContext : DbContext
    { 
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Condition> Conditions { get; set; }
        public virtual DbSet<Faq> Faqs { get; set; }
        public virtual DbSet<Faqtype> Faqtypes { get; set; }
        public virtual DbSet<OtherImage> OtherImages { get; set; }
        public virtual DbSet<Property> Properties { get; set; }
        public virtual DbSet<PropertyOtherImage> PropertyOtherImages { get; set; }
        public virtual DbSet<PropertyType> PropertyTypes { get; set; }
        public virtual DbSet<PropertyWhatsSpecialLink> PropertyWhatsSpecialLinks { get; set; }
        public virtual DbSet<Purpose> Purposes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<WhatsSpecial> WhatsSpecials { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=192.168.1.29,1433;Database=realest;User Id=sa;Password=Ramadani.0402;TrustServerCertificate=True;Persist Security Info=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Condition>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Faq>(entity =>
            {
                entity.ToTable("FAQ");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Answer).IsRequired();

                entity.Property(e => e.Question).IsRequired();

                entity.HasOne(d => d.FaqType)
                    .WithMany(p => p.Faqs)
                    .HasForeignKey(d => d.FaqTypeId)
                    .HasConstraintName("FK__FAQ__FaqTypeId__68487DD7");
            });

            modelBuilder.Entity<Faqtype>(entity =>
            {
                entity.ToTable("FAQType");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<OtherImage>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Base64stringImage)
                    .IsRequired()
                    .HasColumnName("base64stringImage");
            });

            modelBuilder.Entity<Property>(entity =>
            {
                entity.ToTable("Property");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.AllDocuments).HasDefaultValueSql("((0))");

                entity.Property(e => e.BuiltIn).HasMaxLength(50);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy).HasMaxLength(100);

                entity.Property(e => e.DatePosted).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Location).HasMaxLength(255);

                entity.Property(e => e.ModifiedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy).HasMaxLength(100);

                entity.Property(e => e.MonthlyPayment).HasDefaultValueSql("((0))");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PriceRange).HasMaxLength(100);

                entity.Property(e => e.Saves).HasDefaultValueSql("((0))");

                entity.Property(e => e.Views).HasDefaultValueSql("((0))");

                entity.HasOne(d => d.Condition)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.ConditionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Property__Condit__73BA3083");

                entity.HasOne(d => d.PropertyType)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.PropertyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Property__Proper__75A278F5");

                entity.HasOne(d => d.Purpose)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.PurposeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Property__Purpos__76969D2E");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.SellerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Property__Seller__2FCF1A8A");

                entity.HasOne(d => d.WhatsSpecial)
                    .WithMany(p => p.Properties)
                    .HasForeignKey(d => d.WhatsSpecialId)
                    .HasConstraintName("FK__Property__WhatsS__778AC167");
            });

            modelBuilder.Entity<PropertyOtherImage>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.OtherImages)
                    .WithMany(p => p.PropertyOtherImages)
                    .HasForeignKey(d => d.OtherImagesId)
                    .HasConstraintName("FK__PropertyO__Other__7B5B524B");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.PropertyOtherImages)
                    .HasForeignKey(d => d.PropertyId)
                    .HasConstraintName("FK__PropertyO__Prope__7C4F7684");
            });

            modelBuilder.Entity<PropertyType>(entity =>
            {
                entity.ToTable("PropertyType");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<PropertyWhatsSpecialLink>(entity =>
            {
                entity.ToTable("PropertyWhatsSpecialLink");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.Property)
                    .WithMany(p => p.PropertyWhatsSpecialLinks)
                    .HasForeignKey(d => d.PropertyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PropertyW__Prope__01142BA1");

                entity.HasOne(d => d.WhatsSpecial)
                    .WithMany(p => p.PropertyWhatsSpecialLinks)
                    .HasForeignKey(d => d.WhatsSpecialId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PropertyW__Whats__00200768");
            });

            modelBuilder.Entity<Purpose>(entity =>
            {
                entity.ToTable("Purpose");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CreatedBy).HasMaxLength(50);

                entity.Property(e => e.Deleted).HasDefaultValueSql("((0))");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ModifiedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ModifiedBy).HasMaxLength(5);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<WhatsSpecial>(entity =>
            {
                entity.ToTable("WhatsSpecial");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
