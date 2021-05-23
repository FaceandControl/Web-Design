using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(e => e.TagDescriptions)
                .WithOne(e => e.User)
                //.HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(e => e.Comments)
                .WithOne(e => e.UserSender)
                //.HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne(e => e.Status)
                .WithMany(e => e.Users)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
