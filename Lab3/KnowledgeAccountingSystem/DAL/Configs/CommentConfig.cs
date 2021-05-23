using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Configs
{
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            //builder.HasOne(e => e.UserProfile);

            //builder.HasMany(e => e.Tags);

            //builder.HasOne(e => e.User)
            //    .WithMany(e => e.Comments)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasMany(e => e.Tags)
                .WithOne(e => e.Comment)
                //.HasForeignKey(e => e.CommentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
