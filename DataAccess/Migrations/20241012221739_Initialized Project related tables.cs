using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class InitializedProjectrelatedtables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_TeamId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingAttachments_Attachments_AttachmentId",
                table: "MeetingAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingAttachments_Meetings_MeetingGuid",
                table: "MeetingAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_AspNetUsers_MeetingLeaderGuid",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_TeammateMeetings_AspNetUsers_TeammateGuid",
                table: "TeammateMeetings");

            migrationBuilder.DropForeignKey(
                name: "FK_TeammateMeetings_Meetings_MeetingGuid",
                table: "TeammateMeetings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");
            
            //custom code in migration
            migrationBuilder.DropPrimaryKey(
                "PK_Meetings",
                "Meetings"
            );
            
            //custom code in migration
            migrationBuilder.DropPrimaryKey(
                "PK_MeetingAttachments",
                "MeetingAttachments"
            );
           

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TeamId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ManagerGuid",
                table: "Teams",
                newName: "ManagerGUID");

            migrationBuilder.RenameColumn(
                name: "MeetingGuid",
                table: "TeammateMeetings",
                newName: "MeetingGUID");

            migrationBuilder.RenameColumn(
                name: "TeammateGuid",
                table: "TeammateMeetings",
                newName: "TeammateGUID");

            migrationBuilder.RenameIndex(
                name: "IX_TeammateMeetings_MeetingGuid",
                table: "TeammateMeetings",
                newName: "IX_TeammateMeetings_MeetingGUID");

            migrationBuilder.RenameColumn(
                name: "MeetingLeaderGuid",
                table: "Meetings",
                newName: "MeetingLeaderGUID");

            migrationBuilder.RenameColumn(
                name: "Guid",
                table: "Meetings",
                newName: "GUID");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_MeetingLeaderGuid",
                table: "Meetings",
                newName: "IX_Meetings_MeetingLeaderGUID");

            migrationBuilder.RenameColumn(
                name: "AttachmentId",
                table: "MeetingAttachments",
                newName: "AttachmentID");

            migrationBuilder.RenameColumn(
                name: "MeetingGuid",
                table: "MeetingAttachments",
                newName: "MeetingGUID");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingAttachments_AttachmentId",
                table: "MeetingAttachments",
                newName: "IX_MeetingAttachments_AttachmentID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Attachments",
                newName: "ID");

            migrationBuilder.AddColumn<string>(
                name: "GUID",
                table: "Teams",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "MeetingGUID",
                table: "TeammateMeetings",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");


            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Meetings",
                type: "nvarchar(max)",
                maxLength: 10000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GUID",
                table: "Meetings",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "MeetingGUID",
                table: "MeetingAttachments",
                type: "nvarchar(36)",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "TeamGUID",
                table: "AspNetUsers",
                type: "nvarchar(36)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "GUID");

            migrationBuilder.CreateTable(
                name: "ActionHistories",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Action = table.Column<int>(type: "int", nullable: false),
                    Item = table.Column<int>(type: "int", nullable: false),
                    UserGUID = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ItemGUID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionHistories", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ActionHistories_AspNetUsers_UserGUID",
                        column: x => x.UserGUID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    GUID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", maxLength: 10000, nullable: true),
                    State = table.Column<int>(type: "int", nullable: false),
                    TeamGUID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    LastUpdate = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.GUID);
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_Creator",
                        column: x => x.Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_AspNetUsers_LastUpdate",
                        column: x => x.LastUpdate,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Projects_Teams_TeamGUID",
                        column: x => x.TeamGUID,
                        principalTable: "Teams",
                        principalColumn: "GUID");
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    GUID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    Points = table.Column<int>(type: "int", nullable: true),
                    ProjectGUID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    LastUpdate = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.GUID);
                    table.CheckConstraint("CK_Features_Points", "[Points] IN (1, 2, 3, 5, 8, 13)");
                    table.ForeignKey(
                        name: "FK_Features_AspNetUsers_Creator",
                        column: x => x.Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Features_AspNetUsers_LastUpdate",
                        column: x => x.LastUpdate,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Features_Projects_ProjectGUID",
                        column: x => x.ProjectGUID,
                        principalTable: "Projects",
                        principalColumn: "GUID");
                });

            migrationBuilder.CreateTable(
                name: "Sprints",
                columns: table => new
                {
                    GUID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Goal = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    Iteration = table.Column<int>(type: "int", nullable: false),
                    ProjectGUID = table.Column<string>(type: "nvarchar(36)", maxLength: 36, nullable: false),
                    Creator = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    LastUpdate = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sprints", x => x.GUID);
                    table.ForeignKey(
                        name: "FK_Sprints_AspNetUsers_Creator",
                        column: x => x.Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sprints_AspNetUsers_LastUpdate",
                        column: x => x.LastUpdate,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Sprints_Projects_ProjectGUID",
                        column: x => x.ProjectGUID,
                        principalTable: "Projects",
                        principalColumn: "GUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    State = table.Column<int>(type: "int", nullable: false),
                    FeatureGUID = table.Column<string>(type: "nvarchar(36)", nullable: false),
                    Assigne = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Creator = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    LastUpdate = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_Assigne",
                        column: x => x.Assigne,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_Creator",
                        column: x => x.Creator,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_AspNetUsers_LastUpdate",
                        column: x => x.LastUpdate,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Tasks_Features_FeatureGUID",
                        column: x => x.FeatureGUID,
                        principalTable: "Features",
                        principalColumn: "GUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SprintNotes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeammateGUID = table.Column<string>(type: "nvarchar(450)", maxLength: 36, nullable: false),
                    SprintID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SprintNotes", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SprintNotes_AspNetUsers_TeammateGUID",
                        column: x => x.TeammateGUID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SprintNotes_Sprints_SprintID",
                        column: x => x.SprintID,
                        principalTable: "Sprints",
                        principalColumn: "GUID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TeamGUID",
                table: "AspNetUsers",
                column: "TeamGUID");

            migrationBuilder.CreateIndex(
                name: "IX_ActionHistories_UserGUID",
                table: "ActionHistories",
                column: "UserGUID");

            migrationBuilder.CreateIndex(
                name: "IX_Features_Creator",
                table: "Features",
                column: "Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Features_LastUpdate",
                table: "Features",
                column: "LastUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_Features_ProjectGUID",
                table: "Features",
                column: "ProjectGUID");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_Creator",
                table: "Projects",
                column: "Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_LastUpdate",
                table: "Projects",
                column: "LastUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_TeamGUID",
                table: "Projects",
                column: "TeamGUID");

            migrationBuilder.CreateIndex(
                name: "IX_SprintNotes_SprintID",
                table: "SprintNotes",
                column: "SprintID");

            migrationBuilder.CreateIndex(
                name: "IX_SprintNotes_TeammateGUID",
                table: "SprintNotes",
                column: "TeammateGUID");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_Creator",
                table: "Sprints",
                column: "Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_LastUpdate",
                table: "Sprints",
                column: "LastUpdate");

            migrationBuilder.CreateIndex(
                name: "IX_Sprints_ProjectGUID",
                table: "Sprints",
                column: "ProjectGUID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Assigne",
                table: "Tasks",
                column: "Assigne");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Creator",
                table: "Tasks",
                column: "Creator");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_FeatureGUID",
                table: "Tasks",
                column: "FeatureGUID");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_LastUpdate",
                table: "Tasks",
                column: "LastUpdate");

            //custom migration code
            migrationBuilder.AddPrimaryKey(
                name: "PK_Meetings",
                table: "Meetings",
                column: "GUID");
            
            //custom migration code
            migrationBuilder.AddPrimaryKey(
                name: "PK_MeetingAttachments",
                table: "MeetingAttachments",
                columns: ["MeetingGUID", "AttachmentID"]);
            
            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_TeamGUID",
                table: "AspNetUsers",
                column: "TeamGUID",
                principalTable: "Teams",
                principalColumn: "GUID");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingAttachments_Attachments_AttachmentID",
                table: "MeetingAttachments",
                column: "AttachmentID",
                principalTable: "Attachments",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingAttachments_Meetings_MeetingGUID",
                table: "MeetingAttachments",
                column: "MeetingGUID",
                principalTable: "Meetings",
                principalColumn: "GUID");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_AspNetUsers_MeetingLeaderGUID",
                table: "Meetings",
                column: "MeetingLeaderGUID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeammateMeetings_AspNetUsers_TeammateGUID",
                table: "TeammateMeetings",
                column: "TeammateGUID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeammateMeetings_Meetings_MeetingGUID",
                table: "TeammateMeetings",
                column: "MeetingGUID",
                principalTable: "Meetings",
                principalColumn: "GUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Teams_TeamGUID",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingAttachments_Attachments_AttachmentID",
                table: "MeetingAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_MeetingAttachments_Meetings_MeetingGUID",
                table: "MeetingAttachments");

            migrationBuilder.DropForeignKey(
                name: "FK_Meetings_AspNetUsers_MeetingLeaderGUID",
                table: "Meetings");

            migrationBuilder.DropForeignKey(
                name: "FK_TeammateMeetings_AspNetUsers_TeammateGUID",
                table: "TeammateMeetings");

            migrationBuilder.DropForeignKey(
                name: "FK_TeammateMeetings_Meetings_MeetingGUID",
                table: "TeammateMeetings");

            migrationBuilder.DropTable(
                name: "ActionHistories");

            migrationBuilder.DropTable(
                name: "SprintNotes");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Sprints");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Teams",
                table: "Teams");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_TeamGUID",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "GUID",
                table: "Teams");

            migrationBuilder.DropColumn(
                name: "TeamGUID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "ManagerGUID",
                table: "Teams",
                newName: "ManagerGuid");

            migrationBuilder.RenameColumn(
                name: "MeetingGUID",
                table: "TeammateMeetings",
                newName: "MeetingGuid");

            migrationBuilder.RenameColumn(
                name: "TeammateGUID",
                table: "TeammateMeetings",
                newName: "TeammateGuid");

            migrationBuilder.RenameIndex(
                name: "IX_TeammateMeetings_MeetingGUID",
                table: "TeammateMeetings",
                newName: "IX_TeammateMeetings_MeetingGuid");

            migrationBuilder.RenameColumn(
                name: "MeetingLeaderGUID",
                table: "Meetings",
                newName: "MeetingLeaderGuid");

            migrationBuilder.RenameColumn(
                name: "GUID",
                table: "Meetings",
                newName: "Guid");

            migrationBuilder.RenameIndex(
                name: "IX_Meetings_MeetingLeaderGUID",
                table: "Meetings",
                newName: "IX_Meetings_MeetingLeaderGuid");

            migrationBuilder.RenameColumn(
                name: "AttachmentID",
                table: "MeetingAttachments",
                newName: "AttachmentId");

            migrationBuilder.RenameColumn(
                name: "MeetingGUID",
                table: "MeetingAttachments",
                newName: "MeetingGuid");

            migrationBuilder.RenameIndex(
                name: "IX_MeetingAttachments_AttachmentID",
                table: "MeetingAttachments",
                newName: "IX_MeetingAttachments_AttachmentId");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Attachments",
                newName: "Id");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Teams",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<string>(
                name: "MeetingGuid",
                table: "TeammateMeetings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "TeammateGuid",
                table: "TeammateMeetings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "MeetingLeaderGuid",
                table: "Meetings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 10000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Guid",
                table: "Meetings",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<string>(
                name: "MeetingGuid",
                table: "MeetingAttachments",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(36)",
                oldMaxLength: 36);

            migrationBuilder.AddColumn<int>(
                name: "TeamId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Teams",
                table: "Teams",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_TeamId",
                table: "AspNetUsers",
                column: "TeamId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Teams_TeamId",
                table: "AspNetUsers",
                column: "TeamId",
                principalTable: "Teams",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingAttachments_Attachments_AttachmentId",
                table: "MeetingAttachments",
                column: "AttachmentId",
                principalTable: "Attachments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MeetingAttachments_Meetings_MeetingGuid",
                table: "MeetingAttachments",
                column: "MeetingGuid",
                principalTable: "Meetings",
                principalColumn: "Guid");

            migrationBuilder.AddForeignKey(
                name: "FK_Meetings_AspNetUsers_MeetingLeaderGuid",
                table: "Meetings",
                column: "MeetingLeaderGuid",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeammateMeetings_AspNetUsers_TeammateGuid",
                table: "TeammateMeetings",
                column: "TeammateGuid",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TeammateMeetings_Meetings_MeetingGuid",
                table: "TeammateMeetings",
                column: "MeetingGuid",
                principalTable: "Meetings",
                principalColumn: "Guid");
        }
    }
}
