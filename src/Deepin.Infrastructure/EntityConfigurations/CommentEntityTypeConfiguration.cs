using Deepin.Domain.CommentAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deepin.Infrastructure.EntityConfigurations;
public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.ToTable("comments");
        builder.Property(s => s.Id).HasColumnName("id");
        builder.Property(x => x.Content).HasColumnName("content").IsRequired();
        builder.Property(x => x.PostId).HasColumnName("post_id");
        builder.Property(x => x.ParentId).HasColumnName("parent_id");
        builder.Property(x => x.IsApproved).HasColumnName("is_approved");
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp with time zone").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
        builder.Property(x => x.UpdatedAt).HasColumnName("update_at").HasColumnType("timestamp with time zone").ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()");
    }
}
