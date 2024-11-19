using Deepin.Domain.Entities;
using Deepin.Domain.PostAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deepin.Infrastructure.EntityConfigurations;

public class PostTagEntityTypeConfiguration : IEntityTypeConfiguration<PostTag>
{
    public void Configure(EntityTypeBuilder<PostTag> builder)
    {
        builder.ToTable("post_tags");

        builder.HasKey(s => new { s.PostId, s.TagId });
        builder.Property(pt => pt.TagId).HasColumnName("tag_id");
        builder.Property(pt => pt.PostId).HasColumnName("post_id");
        builder.HasOne<Post>().WithMany(p => p.PostTags).HasForeignKey(s => s.PostId);
        builder.HasOne<Tag>().WithMany().HasForeignKey(s => s.TagId);

    }

}
