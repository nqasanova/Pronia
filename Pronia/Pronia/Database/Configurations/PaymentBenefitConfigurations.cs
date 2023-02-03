using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pronia.Database.Models;

namespace Pronia.Database.Configurations
{
    public class PaymentBenefitConfigurations : IEntityTypeConfiguration<PaymentBenefit>
    {
        public void Configure(EntityTypeBuilder<PaymentBenefit> builder)
        {
            builder
                .ToTable("PaymentBenefits");
        }
    }
}