using CareerFIZ.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerFIZ.Configurations
{
    public class LogConfiguration
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Log");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Action).HasMaxLength(800).IsRequired();
            builder.Property(x => x.ActionTime).HasDefaultValue(System.DateTime.Now);
            builder.Property(x => x.AppUserId).IsRequired();
        }
    }
}
