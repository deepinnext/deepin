namespace Deepin.Domain.NoteAggregates;


public class NoteTag : Entity
{
    public int NoteId { get; set; }
    public int TagId { get; set; }
    public NoteTag(int tagId)
    {
        TagId = tagId;
    }
}