using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BugTicketingSystem.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UsersAndBugManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserBugs",
                columns: table => new
                {
                    User_Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Bug_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserBugs", x => new { x.User_Id, x.Bug_Id });
                    table.ForeignKey(
                        name: "FK_UserBugs_AspNetUsers_User_Id",
                        column: x => x.User_Id,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserBugs_Bugs_Bug_Id",
                        column: x => x.Bug_Id,
                        principalTable: "Bugs",
                        principalColumn: "BugId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserBugs_Bug_Id",
                table: "UserBugs",
                column: "Bug_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserBugs");
        }
    }
}
