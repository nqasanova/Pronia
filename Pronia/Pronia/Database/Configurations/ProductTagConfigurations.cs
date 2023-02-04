using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pronia.Database.Models;

namespace Pronia.Database.Configurations
{
    public class ProductTagConfigurations : IEntityTypeConfiguration<ProductTag>
    {
        public void Configure(EntityTypeBuilder<ProductTag> builder)
        {
            builder
                .ToTable("ProductTags");

            builder
               .HasOne(pi => pi.Product)
               .WithMany(p => p.ProductTags)
               .HasForeignKey(pt => pt.ProductId);
        }
    }
}