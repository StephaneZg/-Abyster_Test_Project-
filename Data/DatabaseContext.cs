
using Abyster_Test_Project.Domain.Account_Journals;
using Abyster_Test_Project.Domain.Accounts;
using Abyster_Test_Project.Domain.Categorys;
using Abyster_Test_Project.Domain.Roles;
using Abyster_Test_Project.Domain.Users;
using Abyster_Test_Project.Service.Contract;
using Abyster_Test_Project.SharedKernel;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Abyster_Test_Project.Data;

public class DatabaseContext : DbContext
{

    private readonly ICurrentUserService _currentUserService;
    private readonly IMediator _mediator;

    private readonly IConfiguration _configuration;

    public DatabaseContext(DbContextOptions options,
     ICurrentUserService currentUserService,
     IMediator mediator,
     IConfiguration configuration) : base(options)
    {
        _mediator = mediator;
        _currentUserService = currentUserService;
        _configuration = configuration;

    }

    protected override void  OnConfiguring(DbContextOptionsBuilder options){

        options.UseSqlite(_configuration.GetConnectionString("AuthSqliteDB"));
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        
        //Property Configurations
            //User
        modelBuilder.Entity<User>()
                .Property(user => user.Id)
                .HasColumnName("user_id");

        modelBuilder.Entity<User>()
                .Property(user => user.isActive)
                .HasDefaultValue(true);

        modelBuilder.Entity<User>()
                .Property(user => user.initialized)
                .HasDefaultValue(false);

            //Account
        modelBuilder.Entity<Account>()
                .Property(account => account.Id)
                .HasColumnName("account_id");

            //Category
        modelBuilder.Entity<Category>()
                .Property(category => category.Id)
                .HasColumnName("category_id");

            //AccountJournal
        modelBuilder.Entity<AccountJournal>()
                .Property(accJournal => accJournal.Id)
                .HasColumnName("account_journal_id");

                 //Role
        modelBuilder.Entity<Role>()
                .Property(role => role.Id)
                .HasColumnName("role_id");

    }


    public DbSet<Account> Accounts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<User> Users {get; set;}
    public DbSet<Role> Roles {get; set;}
    public DbSet<AccountJournal> AccountJournals {get; set;}

    public override int SaveChanges()
    {
        UpdateAuditFields();
        var result = base.SaveChanges();
        _dispatchDomainEvents().GetAwaiter().GetResult();
        return result;
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        UpdateAuditFields();
        var result = await base.SaveChangesAsync(cancellationToken);
        await _dispatchDomainEvents();
        return result;
    }
    
    private async Task _dispatchDomainEvents()
    {
        // var domainEventEntities = ChangeTracker.Entries<Common>()
        //     .Select(po => po.Entity)
        //     .ToArray();

        // foreach (var entity in domainEventEntities)
        // {
        //     var events = entity.DomainEvents.ToArray();
        //     entity.DomainEvents.Clear();
        //     foreach (var entityDomainEvent in events)
        //         await _mediator.Publish(entityDomainEvent);
        // }
    }
        
    private void UpdateAuditFields()
    {
        var now = DateTime.UtcNow;
        foreach (var entry in ChangeTracker.Entries<Common>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    Console.WriteLine("CURRENT USER ID "+_currentUserService?.UserId);
                    entry.Entity.UpdateCreationProperties(now, _currentUserService?.UserId);
                    entry.Entity.UpdateModifiedProperties(now, _currentUserService?.UserId);
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdateModifiedProperties(now, _currentUserService?.UserId);
                    break;
            }
        }
    }
}