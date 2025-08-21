using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WorkSphereAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddTaskComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TaskItemId = table.Column<int>(type: "int", nullable: false),
                    CommentedByUserId = table.Column<int>(type: "int", nullable: false),
                    CommentedById = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskComments_Tasks_TaskItemId",
                        column: x => x.TaskItemId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskComments_Users_CommentedById",
                        column: x => x.CommentedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_CommentedById",
                table: "TaskComments",
                column: "CommentedById");

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_TaskItemId",
                table: "TaskComments",
                column: "TaskItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskComments");
        }
    }
}
