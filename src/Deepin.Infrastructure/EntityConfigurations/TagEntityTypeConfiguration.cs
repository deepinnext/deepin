using Deepin.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Deepin.Infrastructure.EntityConfigurations;
public class TagEntityTypeConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tags");
        builder.Property(s => s.Id).HasColumnName("id");
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.Property(x => x.Description).HasColumnName("description");
        builder.Property(x => x.Slug).HasColumnName("slug");
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp with time zone").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
        builder.Property(x => x.UpdatedAt).HasColumnName("update_at").HasColumnType("timestamp with time zone").ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()");
    }
}
