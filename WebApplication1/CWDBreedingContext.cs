using Microsoft.EntityFrameworkCore;
using ReviewPlatformAPI.Entities;

namespace ReviewPlatformAPI
{
    public partial class CWDBreedingContext : DbContext
    {
        public CWDBreedingContext()
        {
        }
        public CWDBreedingContext(DbContextOptions<CWDBreedingContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }
        public virtual DbSet<ChangePassword> ChangePasswords { get; set; } = null!;
        public virtual DbSet<Deer> Deer { get; set; } = null!;
        public virtual DbSet<LevelOneRelationship> LevelOneRelationships { get; set; } = null!;
        public virtual DbSet<LevelThreeRelationship> LevelThreeRelationships { get; set; } = null!;
        public virtual DbSet<LevelTwoRelationship> LevelTwoRelationships { get; set; } = null!;
        public virtual DbSet<LoginData> LoginData { get; set; } = null!;
        public virtual DbSet<Media> Media { get; set; } = null!;
        public virtual DbSet<Ranch> Ranches { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChangePassword>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Ranch)
                    .WithMany(p => p.ChangePasswords)
                    .HasForeignKey(d => d.RanchId)
                    .HasConstraintName("FK_ChangePasswords_Ranch");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ChangePasswords)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ChangePasswords_Users");
            });

            modelBuilder.Entity<Deer>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Age).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Codon)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("Codon");

                entity.Property(e => e.Dob)
                    .HasColumnType("datetime")
                    .HasColumnName("DOB");

                entity.Property(e => e.Gebu)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("GEBU");

                entity.Property(e => e.Nadr)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NADR");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SciScore).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.SemenCost)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Ranch)
                    .WithMany(p => p.Deer)
                    .HasForeignKey(d => d.RanchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Deer_Ranch");
            });

            modelBuilder.Entity<LevelOneRelationship>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Dam)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sire)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Deer)
                    .WithMany(p => p.LevelOneRelationships)
                    .HasForeignKey(d => d.DeerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LevelOneRelationships_Deer");
            });

            modelBuilder.Entity<LevelThreeRelationship>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DamA)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DamB)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DamC)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DamD)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SireA)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SireB)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SireC)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SireD)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Deer)
                    .WithMany(p => p.LevelThreeRelationships)
                    .HasForeignKey(d => d.DeerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LevelThreeRelationships_Deer");
            });

            modelBuilder.Entity<LevelTwoRelationship>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DamA)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DamB)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SireA)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.SireB)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Deer)
                    .WithMany(p => p.LevelTwoRelationships)
                    .HasForeignKey(d => d.DeerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LevelTwoRelationships_Deer");
            });

            modelBuilder.Entity<LoginData>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Salt)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Media>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BlobId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Deer)
                    .WithMany(p => p.Media)
                    .HasForeignKey(d => d.DeerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Media_Deer");
            });

            modelBuilder.Entity<Ranch>(entity =>
            {
                entity.ToTable("Ranch");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Website)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Zipcode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.LoginData)
                    .WithMany(p => p.Ranches)
                    .HasForeignKey(d => d.LoginDataId)
                    .HasConstraintName("FK_Ranch_LoginData");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.HasOne(d => d.LoginData)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LoginDataId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_LoginData");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
