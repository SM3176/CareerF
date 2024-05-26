using CareerFIZ.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerFIZ.Configurations
{
    public class CVConfiguration : IEntityTypeConfiguration<Cv>
    {
        public void Configure(EntityTypeBuilder<Cv> builder)
        {
            builder.ToTable("CVs");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            //CV
            builder.Property(x => x.Certificate).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Major).HasMaxLength(100).IsRequired();
            builder.Property(x => x.GraduatedAt).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Phone).HasMaxLength(20).IsRequired();
            builder.Property(x => x.Email).HasMaxLength(50).IsRequired();
            builder.Property(x => x.Gpa).IsRequired();
            builder.Property(x => x.Description).IsRequired();
            builder.Property(x => x.Introduce).IsRequired();
            //Feedback
            builder.Property(x => x.City).HasMaxLength(30);
            builder.Property(x => x.EmployerEmail).HasMaxLength(50);
            builder.Property(x => x.EmployerPhone).HasMaxLength(20);
            builder.Property(x => x.Status).HasDefaultValue(0);
        }
    }
}