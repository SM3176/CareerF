using CareerFIZ.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerFIZ.Configurations
{
    public class PaymentConfiguration
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payment");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();
            builder.Property(x => x.Amount).HasPrecision(18,2).IsRequired();
            builder.Property(x => x.PaymentDate).HasDefaultValue(System.DateTime.Now);
            builder.Property(x => x.AppUserId).IsRequired();
        }
    }
}
