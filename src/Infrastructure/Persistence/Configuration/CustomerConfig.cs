using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDo.Domain.CustomerAggregate;
using System.Reflection.Emit;
using ToDo.Domain.Common.ValueObjects;

namespace Infrastructure.Persistence.Configuration
{
    public class CustomerConfig : IEntityTypeConfiguration<CustomerItem>
    {
        public void Configure(EntityTypeBuilder<CustomerItem> builder)
        {
            builder.ToTable("CustomerItems");
            builder.HasKey(ci => ci.Id);
         

            builder.OwnsOne(c => c.FullName, d =>
            {
                d.Property(e => e.FirstName).HasMaxLength(150).IsRequired().HasColumnName("FirstName");
                d.Property(e => e.LastName).HasMaxLength(150).IsRequired().HasColumnName("LastName");
            });

            builder.Property(c => c.PhoneNumber)
              .HasMaxLength(11)
              .IsUnicode(false)
              .HasConversion(c => c.Value, d => PhoneNumber.Create(d).Value);


            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasConversion(c => c.Value, d => Email.Create(d).Value);

            builder.Property(c => c.BankAccountNumber)
               .IsRequired()
               .HasMaxLength(14)
               .IsUnicode(false)
               .HasConversion(c => c.Value, d => AccountNumber.Create(d).Value);

        }
    }
}