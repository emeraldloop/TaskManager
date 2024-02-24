using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Aggregates.Bases.Entities;

namespace TaskManager.DataSource.Configurations;

public abstract class DomainEntityConfiguration<TEntity>
    : Configuration, IEntityTypeConfiguration<TEntity>
    where TEntity : DomainEntity
{
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
        builder.Property(x => x.Id).ValueGeneratedNever();

        builder.HasIndex(x => x.DateCreated);

        builder.HasIndex(x => x.DateDeleted);
        builder
            .Ignore(x => x.IsDeleted)
            .HasQueryFilter(x => !x.DateDeleted.HasValue);

        ConfigureAdvanced(builder);
    }

    protected abstract void ConfigureAdvanced(EntityTypeBuilder<TEntity> builder);
}