using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Configs
{
    public class TagDescriptionConfig : IEntityTypeConfiguration<TagDescription>
    {
        public void Configure(EntityTypeBuilder<TagDescription> builder)
        {
            //builder.HasOne(e => e.UserProfile)
            //    .WithMany(e => e.TagDescriptions)
            //    .OnDelete(DeleteBehavior.ClientSetNull);

            //builder.HasOne(e => e.Tag)
            //    .WithMany(e => e.TagDescriptions)
            //    .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
