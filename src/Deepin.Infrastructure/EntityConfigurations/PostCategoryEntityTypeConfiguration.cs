using System;
using Deepin.Domain.Entities;
using Deepin.Domain.PostAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deepin.Infrastructure.EntityConfigurations;

public class PostCategoryEntityTypeConfiguration : IEntityTypeConfiguration<PostCategory>
{
    public void Configure(EntityTypeBuilder<PostCategory> builder)
    {
        builder.ToTable("post_categories");
        builder.HasKey(s => new { s.PostId, s.CategoryId });
        builder.Property(s => s.PostId).HasColumnName("post_id");
        builder.Property(s => s.CategoryId).HasColumnName("category_id");
        builder.HasOne<Post>().WithMany(p => p.PostCategories).HasForeignKey(s => s.PostId);
        builder.HasOne<Category>().WithMany().HasForeignKey(s => s.CategoryId);
    }
}
