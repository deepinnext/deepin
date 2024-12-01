using System.Data;
using Deepin.Domain;
using Deepin.Domain.CommentAggregates;
using Deepin.Domain.Entities;
using Deepin.Domain.PageAggregates;
using Deepin.Domain.PostAggregates;
using Deepin.Domain.RoleAggregates;
using Deepin.Domain.UserAggregates;
using Deepin.Infrastructure.EntityConfigurations;
using Deepin.Infrastructure.Extensions;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage;

namespace Deepin.Infrastructure;
public class DeepinDbContext(DbContextOptions options, IMediator? mediator = null) : IdentityDbContext<User, Role, Guid, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>(options), IUnitOfWork
{
    private readonly IMediator? _mediator = mediator;
    private IDbContextTransaction? _currentTransaction;
    public bool HasActiveTransaction => _currentTransaction != null;
    public DbSet<FileObject> FileObjects { get; set; } = null!;
    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Tag> Tags { get; set; } = null!;
    public DbSet<Post> Posts { get; set; } = null!;
    public DbSet<Note> Notes { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema("deepin");
        modelBuilder.ApplyConfiguration(new FileObjectEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CategoryEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TagEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PostEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PostTagEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CommentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CommentLikeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new NoteEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new NoteTagEntityTypeConfiguration());

        modelBuilder.Entity<Role>(b =>
        {
            b.ToTable("roles");
            b.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
            b.Property(x => x.UpdatedAt).HasColumnType("timestamp with time zone").ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()");

        });
        modelBuilder.Entity<RoleClaim>().ToTable("role_claims");
        modelBuilder.Entity<User>(b =>
        {
            b.ToTable("users");
            b.HasIndex(x => x.UserName).IsUnique();
            b.HasMany(x => x.UserRoles).WithOne().HasForeignKey(r => r.UserId);
            b.HasMany(x => x.UserClaims).WithOne().HasForeignKey(r => r.UserId);
            b.HasMany(x => x.UserLogins).WithOne().HasForeignKey(r => r.UserId);
            b.HasMany(x => x.UserTokens).WithOne().HasForeignKey(r => r.UserId);
            b.Property(x => x.CreatedAt).HasColumnType("timestamp with time zone").ValueGeneratedOnAdd().HasDefaultValueSql("now()");
            b.Property(x => x.UpdatedAt).HasColumnType("timestamp with time zone").ValueGeneratedOnAddOrUpdate().HasDefaultValueSql("now()");
        });
        modelBuilder.Entity<UserRole>().ToTable("user_roles");
        modelBuilder.Entity<UserLogin>().ToTable("user_logins");
        modelBuilder.Entity<UserClaim>().ToTable("user_claims");
        modelBuilder.Entity<UserToken>().ToTable("user_tokens");
    }
    public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default)
    {
        // Dispatch Domain Events collection. 
        // Choices:
        // A) Right BEFORE committing data (EF SaveChanges) into the DB will make a single transaction including  
        // side effects from the domain event handlers which are using the same DbContext with "InstancePerLifetimeScope" or "scoped" lifetime
        // B) Right AFTER committing data (EF SaveChanges) into the DB will make multiple transactions. 
        // You will need to handle eventual consistency and compensatory actions in case of failures in any of the Handlers. 
        if (_mediator != null)
            await _mediator.DispatchDomainEventsAsync(this);

        // After executing this line all the changes (from the Command Handler and Domain Event Handlers) 
        // performed through the DbContext will be committed
        _ = await base.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        if (_currentTransaction != null) return _currentTransaction;

        _currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

        return _currentTransaction;
    }

    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        if (transaction != null)
        {
            if (transaction != _currentTransaction) throw new InvalidOperationException($"Transaction {transaction.TransactionId} is not current");

            try
            {
                await SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
        else
        {
            throw new ArgumentNullException(nameof(transaction));
        }
    }

    public void RollbackTransaction()
    {
        try
        {
            _currentTransaction?.Rollback();
        }
        finally
        {
            if (_currentTransaction != null)
            {
                _currentTransaction.Dispose();
                _currentTransaction = null;
            }
        }
    }
}
public class DeepinDbContextFactory : IDesignTimeDbContextFactory<DeepinDbContext>
{
    public DeepinDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DeepinDbContext>();
        optionsBuilder.UseNpgsql("Database=deepin");
        return new DeepinDbContext(optionsBuilder.Options);
    }
}
