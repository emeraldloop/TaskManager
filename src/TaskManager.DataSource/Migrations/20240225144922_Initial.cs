using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.DataSource.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkTasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskStatus = table.Column<string>(type: "character varying(10)", nullable: false),
                    DateFinishScheduled = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DateDeleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkTasks", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkTasks_DateCreated",
                table: "WorkTasks",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_WorkTasks_DateDeleted",
                table: "WorkTasks",
                column: "DateDeleted");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkTasks");
        }
    }
}
