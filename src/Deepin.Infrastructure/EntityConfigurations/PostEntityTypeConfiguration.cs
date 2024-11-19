using Deepin.Domain.PostAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deepin.Infrastructure.EntityConfigurations;
public class PostEntityTypeConfiguration : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");
        builder.Property(s => s.Id).HasColumnName("id");
        builder.Property(x => x.Title).HasColumnName("title").IsRequired();
        builder.Property(x => x.Summary).HasColumnName("summary");
        builder.Property(x => x.Content).HasColumnName("content");
        builder.Property(x => x.Slug).HasColumnName("slug");
        builder.Property(x => x.Status).HasConversion<string>().HasColumnName("status");
        builder.Property(x => x.Slug).HasColumnName("slug");
        builder.Property(x => x.IsPublic).HasColumnName("is_public");
        builder.Property(x => x.PublishedAt).HasColumnName("published_at").HasColumnType("timestamp with time zone");
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp with time zone").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
        builder.Property(x => x.UpdatedAt).HasColumnName("update_at").HasColumnType("timestamp with time zone").ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()");
    }
}
