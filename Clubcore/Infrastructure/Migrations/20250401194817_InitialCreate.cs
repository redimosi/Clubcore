using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clubcore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clubs",
                columns: table => new
                {
                    ClubId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clubs", x => x.ClubId);
                });

            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name_FirstName = table.Column<string>(type: "text", nullable: false),
                    Name_LastName = table.Column<string>(type: "text", nullable: false),
                    Name_MobileNr = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.GroupId);
                    table.ForeignKey(
                        name: "FK_Groups_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId");
                });

            migrationBuilder.CreateTable(
                name: "ClubGroup",
                columns: table => new
                {
                    ClubId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClubGroup", x => new { x.ClubId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_ClubGroup_Clubs_ClubId",
                        column: x => x.ClubId,
                        principalTable: "Clubs",
                        principalColumn: "ClubId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClubGroup_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GroupRelationship",
                columns: table => new
                {
                    ParentGroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    ChildGroupId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupRelationship", x => new { x.ChildGroupId, x.ParentGroupId });
                    table.ForeignKey(
                        name: "FK_GroupRelationship_Groups_ChildGroupId",
                        column: x => x.ChildGroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupRelationship_Groups_ParentGroupId",
                        column: x => x.ParentGroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonRole",
                columns: table => new
                {
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonRole", x => new { x.PersonId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_PersonRole_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "GroupId");
                    table.ForeignKey(
                        name: "FK_PersonRole_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeRange",
                columns: table => new
                {
                    TimeRangeId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Start = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    End = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ClubGroupClubId = table.Column<Guid>(type: "uuid", nullable: true),
                    ClubGroupGroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    GroupRelationshipChildGroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    GroupRelationshipParentGroupId = table.Column<Guid>(type: "uuid", nullable: true),
                    PersonRolePersonId = table.Column<Guid>(type: "uuid", nullable: true),
                    PersonRoleRoleId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeRange", x => x.TimeRangeId);
                    table.ForeignKey(
                        name: "FK_TimeRange_ClubGroup_ClubGroupClubId_ClubGroupGroupId",
                        columns: x => new { x.ClubGroupClubId, x.ClubGroupGroupId },
                        principalTable: "ClubGroup",
                        principalColumns: new[] { "ClubId", "GroupId" });
                    table.ForeignKey(
                        name: "FK_TimeRange_GroupRelationship_GroupRelationshipChildGroupId_G~",
                        columns: x => new { x.GroupRelationshipChildGroupId, x.GroupRelationshipParentGroupId },
                        principalTable: "GroupRelationship",
                        principalColumns: new[] { "ChildGroupId", "ParentGroupId" });
                    table.ForeignKey(
                        name: "FK_TimeRange_PersonRole_PersonRolePersonId_PersonRoleRoleId",
                        columns: x => new { x.PersonRolePersonId, x.PersonRoleRoleId },
                        principalTable: "PersonRole",
                        principalColumns: new[] { "PersonId", "RoleId" });
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TimeRangeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_TimeRange_TimeRangeId",
                        column: x => x.TimeRangeId,
                        principalTable: "TimeRange",
                        principalColumn: "TimeRangeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    FeedbackId = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonId = table.Column<Guid>(type: "uuid", nullable: false),
                    EventId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.FeedbackId);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClubGroup_GroupId",
                table: "ClubGroup",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_TimeRangeId",
                table: "Events",
                column: "TimeRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_EventId",
                table: "Feedbacks",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_PersonId",
                table: "Feedbacks",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupRelationship_ParentGroupId",
                table: "GroupRelationship",
                column: "ParentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_PersonId",
                table: "Groups",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRole_GroupId",
                table: "PersonRole",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonRole_RoleId",
                table: "PersonRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeRange_ClubGroupClubId_ClubGroupGroupId",
                table: "TimeRange",
                columns: new[] { "ClubGroupClubId", "ClubGroupGroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeRange_GroupRelationshipChildGroupId_GroupRelationshipPa~",
                table: "TimeRange",
                columns: new[] { "GroupRelationshipChildGroupId", "GroupRelationshipParentGroupId" });

            migrationBuilder.CreateIndex(
                name: "IX_TimeRange_PersonRolePersonId_PersonRoleRoleId",
                table: "TimeRange",
                columns: new[] { "PersonRolePersonId", "PersonRoleRoleId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "TimeRange");

            migrationBuilder.DropTable(
                name: "ClubGroup");

            migrationBuilder.DropTable(
                name: "GroupRelationship");

            migrationBuilder.DropTable(
                name: "PersonRole");

            migrationBuilder.DropTable(
                name: "Clubs");

            migrationBuilder.DropTable(
                name: "Groups");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Persons");
        }
    }
}
