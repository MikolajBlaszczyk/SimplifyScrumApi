using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Modelupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_AspNetUsers_Creator",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Features_AspNetUsers_LastUpdate",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_Creator",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_LastUpdate",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Sprints_AspNetUsers_Creator",
                table: "Sprints");

            migrationBuilder.DropForeignKey(
                name: "FK_Sprints_AspNetUsers_LastUpdate",
                table: "Sprints");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_Assigne",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_Creator",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_LastUpdate",
                table: "Tasks");

            migrationBuilder.RenameColumn(
                name: "LastUpdate",
                table: "Tasks",
                newName: "LastUpdatedBy");

            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "Tasks",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "Assigne",
                table: "Tasks",
                newName: "Assignee");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_LastUpdate",
                table: "Tasks",
                newName: "IX_Tasks_LastUpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_Creator",
                table: "Tasks",
                newName: "IX_Tasks_CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_Assigne",
                table: "Tasks",
                newName: "IX_Tasks_Assignee");

            migrationBuilder.RenameColumn(
                name: "LastUpdate",
                table: "Sprints",
                newName: "LastUpdatedBy");

            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "Sprints",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Sprints_LastUpdate",
                table: "Sprints",
                newName: "IX_Sprints_LastUpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Sprints_Creator",
                table: "Sprints",
                newName: "IX_Sprints_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "LastUpdate",
                table: "Projects",
                newName: "LastUpdatedBy");

            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "Projects",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_LastUpdate",
                table: "Projects",
                newName: "IX_Projects_LastUpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_Creator",
                table: "Projects",
                newName: "IX_Projects_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "LastUpdate",
                table: "Features",
                newName: "LastUpdatedBy");

            migrationBuilder.RenameColumn(
                name: "Creator",
                table: "Features",
                newName: "CreatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Features_LastUpdate",
                table: "Features",
                newName: "IX_Features_LastUpdatedBy");

            migrationBuilder.RenameIndex(
                name: "IX_Features_Creator",
                table: "Features",
                newName: "IX_Features_CreatedBy");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateOn",
                table: "Tasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Sprints",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateOn",
                table: "Sprints",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateOn",
                table: "Projects",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Features",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateOn",
                table: "Features",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Features_AspNetUsers_CreatedBy",
                table: "Features",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_AspNetUsers_LastUpdatedBy",
                table: "Features",
                column: "LastUpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_CreatedBy",
                table: "Projects",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_LastUpdatedBy",
                table: "Projects",
                column: "LastUpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sprints_AspNetUsers_CreatedBy",
                table: "Sprints",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sprints_AspNetUsers_LastUpdatedBy",
                table: "Sprints",
                column: "LastUpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_Assignee",
                table: "Tasks",
                column: "Assignee",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedBy",
                table: "Tasks",
                column: "CreatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_LastUpdatedBy",
                table: "Tasks",
                column: "LastUpdatedBy",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Features_AspNetUsers_CreatedBy",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Features_AspNetUsers_LastUpdatedBy",
                table: "Features");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_CreatedBy",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_AspNetUsers_LastUpdatedBy",
                table: "Projects");

            migrationBuilder.DropForeignKey(
                name: "FK_Sprints_AspNetUsers_CreatedBy",
                table: "Sprints");

            migrationBuilder.DropForeignKey(
                name: "FK_Sprints_AspNetUsers_LastUpdatedBy",
                table: "Sprints");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_Assignee",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_CreatedBy",
                table: "Tasks");

            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_AspNetUsers_LastUpdatedBy",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "LastUpdateOn",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "LastUpdateOn",
                table: "Sprints");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "LastUpdateOn",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Features");

            migrationBuilder.DropColumn(
                name: "LastUpdateOn",
                table: "Features");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "Tasks",
                newName: "LastUpdate");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Tasks",
                newName: "Creator");

            migrationBuilder.RenameColumn(
                name: "Assignee",
                table: "Tasks",
                newName: "Assigne");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_LastUpdatedBy",
                table: "Tasks",
                newName: "IX_Tasks_LastUpdate");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_CreatedBy",
                table: "Tasks",
                newName: "IX_Tasks_Creator");

            migrationBuilder.RenameIndex(
                name: "IX_Tasks_Assignee",
                table: "Tasks",
                newName: "IX_Tasks_Assigne");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "Sprints",
                newName: "LastUpdate");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Sprints",
                newName: "Creator");

            migrationBuilder.RenameIndex(
                name: "IX_Sprints_LastUpdatedBy",
                table: "Sprints",
                newName: "IX_Sprints_LastUpdate");

            migrationBuilder.RenameIndex(
                name: "IX_Sprints_CreatedBy",
                table: "Sprints",
                newName: "IX_Sprints_Creator");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "Projects",
                newName: "LastUpdate");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Projects",
                newName: "Creator");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_LastUpdatedBy",
                table: "Projects",
                newName: "IX_Projects_LastUpdate");

            migrationBuilder.RenameIndex(
                name: "IX_Projects_CreatedBy",
                table: "Projects",
                newName: "IX_Projects_Creator");

            migrationBuilder.RenameColumn(
                name: "LastUpdatedBy",
                table: "Features",
                newName: "LastUpdate");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Features",
                newName: "Creator");

            migrationBuilder.RenameIndex(
                name: "IX_Features_LastUpdatedBy",
                table: "Features",
                newName: "IX_Features_LastUpdate");

            migrationBuilder.RenameIndex(
                name: "IX_Features_CreatedBy",
                table: "Features",
                newName: "IX_Features_Creator");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_AspNetUsers_Creator",
                table: "Features",
                column: "Creator",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Features_AspNetUsers_LastUpdate",
                table: "Features",
                column: "LastUpdate",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_Creator",
                table: "Projects",
                column: "Creator",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_AspNetUsers_LastUpdate",
                table: "Projects",
                column: "LastUpdate",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sprints_AspNetUsers_Creator",
                table: "Sprints",
                column: "Creator",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sprints_AspNetUsers_LastUpdate",
                table: "Sprints",
                column: "LastUpdate",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_Assigne",
                table: "Tasks",
                column: "Assigne",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_Creator",
                table: "Tasks",
                column: "Creator",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_AspNetUsers_LastUpdate",
                table: "Tasks",
                column: "LastUpdate",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
