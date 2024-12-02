using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Nameadjustments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SprintNotes_Sprints_SprintID",
                table: "SprintNotes");

            migrationBuilder.RenameColumn(
                name: "SprintID",
                table: "SprintNotes",
                newName: "SprintGUID");

            migrationBuilder.RenameIndex(
                name: "IX_SprintNotes_SprintID",
                table: "SprintNotes",
                newName: "IX_SprintNotes_SprintGUID");

            migrationBuilder.AddForeignKey(
                name: "FK_SprintNotes_Sprints_SprintGUID",
                table: "SprintNotes",
                column: "SprintGUID",
                principalTable: "Sprints",
                principalColumn: "GUID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SprintNotes_Sprints_SprintGUID",
                table: "SprintNotes");

            migrationBuilder.RenameColumn(
                name: "SprintGUID",
                table: "SprintNotes",
                newName: "SprintID");

            migrationBuilder.RenameIndex(
                name: "IX_SprintNotes_SprintGUID",
                table: "SprintNotes",
                newName: "IX_SprintNotes_SprintID");

            migrationBuilder.AddForeignKey(
                name: "FK_SprintNotes_Sprints_SprintID",
                table: "SprintNotes",
                column: "SprintID",
                principalTable: "Sprints",
                principalColumn: "GUID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
