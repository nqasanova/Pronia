using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pronia.Database.Models;

namespace Pronia.Database.Configurations
{
    public class RewardConfigurations : IEntityTypeConfiguration<Reward>
    {
        public void Configure(EntityTypeBuilder<Reward> builder)
        {
            builder
                .ToTable("Rewards");
        }
    }
}