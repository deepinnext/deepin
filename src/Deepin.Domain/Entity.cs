using MediatR;

namespace Deepin.Domain;
public abstract class Entity : IEntity
{
    private List<INotification> _domainEvents = [];
    public IReadOnlyCollection<INotification> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(INotification eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(INotification eventItem)
    {
        _domainEvents.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
public abstract class Entity<T> : Entity
{
    int? _requestedHashCode;
    T _id;
    public virtual T Id
    {
        get
        {
            return _id;
        }
        set
        {
            _id = value;
        }
    }

    public bool IsTransient()
    {
        return EqualityComparer<T>.Default.Equals(Id, default);
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Entity))
            return false;

        if (Object.ReferenceEquals(this, obj))
            return true;

        if (this.GetType() != obj.GetType())
            return false;

        Entity<T> item = (Entity<T>)obj;

        if (item.IsTransient() || this.IsTransient())
            return false;
        else
            return EqualityComparer<T>.Default.Equals(item.Id, this.Id);
    }

    public override int GetHashCode()
    {
        if (!IsTransient())
        {
            if (!_requestedHashCode.HasValue)
                _requestedHashCode = this.Id.GetHashCode() ^ 31; // XOR for random distribution (http://blogs.msdn.com/b/ericlippert/archive/2011/02/28/guidelines-and-rules-for-gethashcode.aspx)

            return _requestedHashCode.Value;
        }
        else
            return base.GetHashCode();

    }
    public static bool operator ==(Entity<T> left, Entity<T> right)
    {
        if (Object.Equals(left, null))
            return (Object.Equals(right, null)) ? true : false;
        else
            return left.Equals(right);
    }

    public static bool operator !=(Entity<T> left, Entity<T> right)
    {
        return !(left == right);
    }
}
