using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Configs
{
    public class TagConfig : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            //builder.HasMany(e => e.TagDescriptions)
            //    .WithOne(e => e.Tag)
            //    .OnDelete(DeleteBehavior.ClientSetNull);
            
            builder.HasOne(e => e.TagDescription)
                .WithOne(e => e.Tag)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
