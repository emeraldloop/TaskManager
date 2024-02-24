using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskManager.Domain.Aggregates.WorkTask;

namespace TaskManager.DataSource.Configurations.WorkTasks;

public class WorkTaskConfiguration
    : DomainEntityConfiguration<WorkTask>
{
    protected override void ConfigureAdvanced(EntityTypeBuilder<WorkTask> builder)
    {
        builder.Property(x => x.TaskStatus).HasConversion(GetStringConverter<WorkTaskStatus>(10));
    }
}