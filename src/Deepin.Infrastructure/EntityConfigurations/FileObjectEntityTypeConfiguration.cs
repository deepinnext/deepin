using System;
using Deepin.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deepin.Infrastructure.EntityConfigurations;

public class FileObjectEntityTypeConfiguration : IEntityTypeConfiguration<FileObject>
{
    public void Configure(EntityTypeBuilder<FileObject> builder)
    {
        builder.ToTable("file_objects");
        builder.Property(s => s.Id).HasColumnName("id").HasColumnType("uuid");
        builder.Property(x => x.Name).HasColumnName("name").IsRequired();
        builder.Property(x => x.Format).HasColumnName("format").IsRequired();
        builder.Property(x => x.Length).HasColumnName("length");
        builder.Property(x => x.Path).HasColumnName("path").IsRequired();
        builder.Property(x => x.CreatedBy).HasColumnName("created_by").IsRequired();
        builder.Property(x => x.CreatedAt).HasColumnName("created_at").HasColumnType("timestamp with time zone").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
        builder.Property(x => x.UpdatedAt).HasColumnName("update_at").HasColumnType("timestamp with time zone").ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()");
    }
}
