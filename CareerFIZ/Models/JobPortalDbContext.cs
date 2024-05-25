using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CareerFIZ.Models
{
    public partial class JobPortalDbContext : DbContext
    {
        public JobPortalDbContext()
        {
        }

        public JobPortalDbContext(DbContextOptions<JobPortalDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AppRole> AppRoles { get; set; } = null!;
        public virtual DbSet<AppRoleClaim> AppRoleClaims { get; set; } = null!;
        public virtual DbSet<AppUser> AppUsers { get; set; } = null!;
        public virtual DbSet<AppUserClaim> AppUserClaims { get; set; } = null!;
        public virtual DbSet<AppUserLogin> AppUserLogins { get; set; } = null!;
        public virtual DbSet<AppUserToken> AppUserTokens { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Country> Countries { get; set; } = null!;
        public virtual DbSet<Cv> Cvs { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Log> Logs { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<Skill> Skills { get; set; } = null!;
        public virtual DbSet<Time> Times { get; set; } = null!;
        public virtual DbSet<Title> Titles { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("server=ASO\\SQLEXPRESS;Database=JobPortalDb;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AppRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AppRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AppRoleClaims)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<AppUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.CategoryId, "IX_AppUsers_CategoryId");

                entity.HasIndex(e => e.CountryId, "IX_AppUsers_CountryId");

                entity.HasIndex(e => e.ProvinceId, "IX_AppUsers_ProvinceId");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Disable).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.Property(e => e.WebsiteUrl)
                    .HasMaxLength(50)
                    .HasColumnName("WebsiteURL");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Country)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.CountryId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.AppUsers)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AppUserRole",
                        l => l.HasOne<AppRole>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull),
                        r => r.HasOne<AppUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AppUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AppUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AppUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AppUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AppUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AppUserLogin>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AppUserLogin)
                    .HasForeignKey<AppUserLogin>(d => d.UserId);
            });

            modelBuilder.Entity<AppUserToken>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.UserId).ValueGeneratedNever();

                entity.HasOne(d => d.User)
                    .WithOne(p => p.AppUserToken)
                    .HasForeignKey<AppUserToken>(d => d.UserId);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(256);

                entity.Property(e => e.Disable).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.Property(e => e.Disable).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Cv>(entity =>
            {
                entity.ToTable("CVs");

                entity.HasIndex(e => e.AppUserId, "IX_CVs_AppUserId");

                entity.HasIndex(e => e.JobId, "IX_CVs_JobId");

                entity.Property(e => e.Certificate).HasMaxLength(100);

                entity.Property(e => e.City).HasMaxLength(30);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.EmployerEmail).HasMaxLength(50);

                entity.Property(e => e.EmployerPhone).HasMaxLength(20);

                entity.Property(e => e.Gpa).HasColumnName("GPA");

                entity.Property(e => e.GraduatedAt).HasMaxLength(100);

                entity.Property(e => e.Major).HasMaxLength(100);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.Cvs)
                    .HasForeignKey(d => d.AppUserId);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.Cvs)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.HasIndex(e => e.AppUserId, "IX_Jobs_AppUserId");

                entity.HasIndex(e => e.CategoryId, "IX_Jobs_CategoryId");

                entity.HasIndex(e => e.ProvinceId, "IX_Jobs_ProvinceId");

                entity.HasIndex(e => e.TimeId, "IX_Jobs_TimeId");

                entity.HasIndex(e => e.TitleId, "IX_Jobs_TitleId");

                entity.Property(e => e.IsSponser)
                    .IsRequired()
                    .HasColumnName("isSponser")
                    .HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.AppUserId);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.CategoryId);

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.ProvinceId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Time)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.TimeId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Title)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.TitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasMany(d => d.Skills)
                    .WithMany(p => p.Jobs)
                    .UsingEntity<Dictionary<string, object>>(
                        "JobSkill",
                        l => l.HasOne<Skill>().WithMany().HasForeignKey("SkillsId").OnDelete(DeleteBehavior.ClientSetNull),
                        r => r.HasOne<Job>().WithMany().HasForeignKey("JobsId").OnDelete(DeleteBehavior.ClientSetNull),
                        j =>
                        {
                            j.HasKey("JobsId", "SkillsId");

                            j.ToTable("JobSkill");

                            j.HasIndex(new[] { "SkillsId" }, "IX_JobSkill_SkillsId");
                        });
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.ToTable("Log");

                entity.HasIndex(e => e.AppUserId, "IX_Log_AppUserId");

                entity.Property(e => e.AppUserId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Ipaddress).HasColumnName("IPAddress");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.Logs)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("Payment");

                entity.HasIndex(e => e.AppUserId, "IX_Payment_AppUserId");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.AppUserId).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.AppUser)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.AppUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.HasIndex(e => e.CategoryId, "IX_Provinces_CategoryId");

                entity.Property(e => e.CategoryId).HasDefaultValueSql("((4))");

                entity.Property(e => e.Disable).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Provinces)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Skill>(entity =>
            {
                entity.HasIndex(e => e.CategoryId, "IX_Skills_CategoryId");

                entity.Property(e => e.CategoryId).HasDefaultValueSql("((1))");

                entity.Property(e => e.Disable).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Skills)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Time>(entity =>
            {
                entity.Property(e => e.Disable).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Name).HasMaxLength(20);
            });

            modelBuilder.Entity<Title>(entity =>
            {
                entity.HasIndex(e => e.CategoryId, "IX_Titles_CategoryId");

                entity.Property(e => e.CategoryId).HasDefaultValueSql("((2))");

                entity.Property(e => e.Disable).HasDefaultValueSql("(CONVERT([bit],(0)))");

                entity.Property(e => e.Name).HasMaxLength(100);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Titles)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
