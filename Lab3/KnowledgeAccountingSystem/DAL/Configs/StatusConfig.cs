using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Configs
{
    public class StatusConfig : IEntityTypeConfiguration<Status>
    {
        public void Configure(EntityTypeBuilder<Status> builder)
        {
            builder.HasData(new List<Status>
            {
                new Status { Id = -4, UserStatus = "Looking for new opportunities" },
                new Status { Id = -3, UserStatus = "Studing" },
                new Status { Id = -2, UserStatus = "Working" },
                new Status { Id = -1, UserStatus = "On vacation" }
            });

            //builder.HasMany(e => e.UserProfiles)
            //    .WithOne(e => e.Status)
            //    .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
