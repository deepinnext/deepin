using Deepin.Domain.Entities;
using Deepin.Domain.NoteAggregates;
using Deepin.Domain.PageAggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Deepin.Infrastructure.EntityConfigurations;

public class NoteTagEntityTypeConfiguration : IEntityTypeConfiguration<NoteTag>
{
    public void Configure(EntityTypeBuilder<NoteTag> builder)
    {
        builder.ToTable("note_tags");

        builder.HasKey(s => new { s.NoteId, s.TagId });
        builder.Property(pt => pt.TagId).HasColumnName("tag_id");
        builder.Property(pt => pt.NoteId).HasColumnName("note_id");
        builder.HasOne<Note>().WithMany(p => p.NoteTags).HasForeignKey(s => s.NoteId);
        builder.HasOne<Tag>().WithMany().HasForeignKey(s => s.TagId);
    }
}
