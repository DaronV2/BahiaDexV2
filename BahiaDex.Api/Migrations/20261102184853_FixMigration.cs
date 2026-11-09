using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BahiaDex.Api.Migrations
{
    /// <inheritdoc />
    public partial class FixMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    IdGoogleAcc = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.IdGoogleAcc);
                });

            migrationBuilder.CreateTable(
                name: "Binder",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    IdUser = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Binder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Binder_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdGoogleAcc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Envy",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCard = table.Column<Guid>(type: "uuid", nullable: false),
                    IdUser = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Envy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Envy_Card_IdCard",
                        column: x => x.IdCard,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Envy_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdGoogleAcc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HasCard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdUser = table.Column<string>(type: "text", nullable: false),
                    IdCard = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HasCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HasCard_Card_IdCard",
                        column: x => x.IdCard,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HasCard_User_IdUser",
                        column: x => x.IdUser,
                        principalTable: "User",
                        principalColumn: "IdGoogleAcc",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Page",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Format = table.Column<int>(type: "integer", nullable: false),
                    IndexInBinder = table.Column<int>(type: "integer", nullable: false),
                    IdBinder = table.Column<Guid>(type: "uuid", nullable: false),
                    BinderId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Page", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Page_Binder_BinderId",
                        column: x => x.BinderId,
                        principalTable: "Binder",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Page_Binder_IdBinder",
                        column: x => x.IdBinder,
                        principalTable: "Binder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmplacementCard",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    IdCard = table.Column<Guid>(type: "uuid", nullable: false),
                    IdPage = table.Column<Guid>(type: "uuid", nullable: false),
                    IndexInPage = table.Column<int>(type: "integer", nullable: false),
                    PageId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmplacementCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmplacementCard_Card_IdCard",
                        column: x => x.IdCard,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmplacementCard_Page_IdPage",
                        column: x => x.IdPage,
                        principalTable: "Page",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmplacementCard_Page_PageId",
                        column: x => x.PageId,
                        principalTable: "Page",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Binder_IdUser",
                table: "Binder",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_EmplacementCard_IdCard",
                table: "EmplacementCard",
                column: "IdCard");

            migrationBuilder.CreateIndex(
                name: "IX_EmplacementCard_IdPage",
                table: "EmplacementCard",
                column: "IdPage");

            migrationBuilder.CreateIndex(
                name: "IX_EmplacementCard_PageId",
                table: "EmplacementCard",
                column: "PageId");

            migrationBuilder.CreateIndex(
                name: "IX_Envy_IdCard",
                table: "Envy",
                column: "IdCard");

            migrationBuilder.CreateIndex(
                name: "IX_Envy_IdUser",
                table: "Envy",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_HasCard_IdCard",
                table: "HasCard",
                column: "IdCard");

            migrationBuilder.CreateIndex(
                name: "IX_HasCard_IdUser",
                table: "HasCard",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Page_BinderId",
                table: "Page",
                column: "BinderId");

            migrationBuilder.CreateIndex(
                name: "IX_Page_IdBinder",
                table: "Page",
                column: "IdBinder");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmplacementCard");

            migrationBuilder.DropTable(
                name: "Envy");

            migrationBuilder.DropTable(
                name: "HasCard");

            migrationBuilder.DropTable(
                name: "Page");

            migrationBuilder.DropTable(
                name: "Binder");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
