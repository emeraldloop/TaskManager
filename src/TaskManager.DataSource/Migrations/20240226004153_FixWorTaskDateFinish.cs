using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.DataSource.Migrations
{
    /// <inheritdoc />
    public partial class FixWorTaskDateFinish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateFinishScheduled",
                table: "WorkTasks",
                newName: "DateFinished");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTasks_TaskStatus",
                table: "WorkTasks",
                column: "TaskStatus");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_WorkTasks_TaskStatus",
                table: "WorkTasks");

            migrationBuilder.RenameColumn(
                name: "DateFinished",
                table: "WorkTasks",
                newName: "DateFinishScheduled");
        }
    }
}
