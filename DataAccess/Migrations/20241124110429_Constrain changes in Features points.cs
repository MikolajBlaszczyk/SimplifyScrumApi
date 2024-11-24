using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ConstrainchangesinFeaturespoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Features_Points",
                table: "Features");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Features_Points",
                table: "Features",
                sql: "[Points] IN (-1, 1, 2, 3, 5, 8, 13)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Features_Points",
                table: "Features");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Features_Points",
                table: "Features",
                sql: "[Points] IN (1, 2, 3, 5, 8, 13)");
        }
    }
}
