using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class IntoducedconnectionbetweenSprintandFeature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SprintNotes_Sprints_SprintGUID",
                table: "SprintNotes");
            
            migrationBuilder.DropPrimaryKey(
                name: "PK_Sprints",
                table: "Sprints");
            
            migrationBuilder.AlterColumn<string>(
                name: "GUID",
                table: "Sprints",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "SprintGUID",
                table: "SprintNotes",
                type: "nvarchar(36)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Sprints",
                table: "Sprints",
                column: "GUID");
            
            migrationBuilder.AddForeignKey(
                name: "FK_SprintNotes_Sprints_SprintGUID",
                table: "SprintNotes",
                column: "SprintGUID",
                principalTable: "Sprints",
                principalColumn: "GUID",
                onDelete: ReferentialAction.Cascade);
            
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Projects",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AssignedToSprint",
                table: "Features",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SprintFeatures",
                columns: table => new
                {
                    SprintGUID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    FeatureGUID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintFeatures", x => new { x.FeatureGUID, x.SprintGUID })
                        .Annotation("SqlServer:Clustered", true);
                    table.ForeignKey(
                        name: "FK_SprintFeatures_Features_FeatureGUID",
                        column: x => x.FeatureGUID,
                        principalTable: "Features",
                        principalColumn: "GUID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SprintFeatures_Sprints_SprintGUID",
                        column: x => x.SprintGUID,
                        principalTable: "Sprints",
                        principalColumn: "GUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SprintFeatures_FeatureGUID",
                table: "SprintFeatures",
                column: "FeatureGUID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_SprintFeatures_SprintGUID",
                table: "SprintFeatures",
                column: "SprintGUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            
            migrationBuilder.DropTable(
                name: "SprintFeatures");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "AssignedToSprint",
                table: "Features");
            
            migrationBuilder.DropForeignKey(
                name: "FK_SprintNotes_Sprints_SprintGUID",
                table: "SprintNotes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Sprints",
                table: "Sprints");
            
            migrationBuilder.AlterColumn<string>(
                name: "GUID",
                table: "Sprints",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_Sprints",
                table: "Sprints",
                column: "GUID");

            migrationBuilder.AlterColumn<string>(
                name: "SprintGUID",
                table: "SprintNotes",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)");
        }
    }
}
