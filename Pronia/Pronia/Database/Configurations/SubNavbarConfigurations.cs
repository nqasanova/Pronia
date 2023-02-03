using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pronia.Database.Models;

namespace Pronia.Database.Configurations
{
    public class SubNavbarConfigurations : IEntityTypeConfiguration<SubNavbar>
    {
        public void Configure(EntityTypeBuilder<SubNavbar> builder)
        {
            builder
                .ToTable("SubNavbars");

            builder
               .HasOne(sn => sn.Navbar)
               .WithMany(n => n.SubNavbars)
               .HasForeignKey(sn => sn.NavbarId);
        }
    }
}