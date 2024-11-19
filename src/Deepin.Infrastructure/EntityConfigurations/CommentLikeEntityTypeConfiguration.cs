using Deepin.Domain.CommentAggregates;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Deepin.Infrastructure.EntityConfigurations;
public class CommentLikeEntityTypeConfiguration : IEntityTypeConfiguration<CommentLike>
{
    public void Configure(EntityTypeBuilder<CommentLike> builder)
    {
        builder.ToTable("comment_likes");
        builder.Property(s => s.Id).HasColumnName("id");
        builder.Property(s => s.CommentId).HasColumnName("comment_id");
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp with time zone").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
    }
}
