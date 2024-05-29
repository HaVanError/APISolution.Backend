using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APISolution.Database.Migrations
{
    /// <inheritdoc />
    public partial class One : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quyen",
                columns: table => new
                {
                    idRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRole = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quyen", x => x.idRole);
                });

            migrationBuilder.CreateTable(
                name: "UserInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "50"),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "200"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false, defaultValue: "100"),
                    IdRole = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserInformation_Quyen_IdRole",
                        column: x => x.IdRole,
                        principalTable: "Quyen",
                        principalColumn: "idRole",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResfreshTokens",
                columns: table => new
                {
                    IdToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Expires = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Accesstoken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Resfreshtoken = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    idUser = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResfreshTokens", x => x.IdToken);
                    table.ForeignKey(
                        name: "FK_ResfreshTokens_UserInformation_idUser",
                        column: x => x.idUser,
                        principalTable: "UserInformation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Quyen_idRole_NameRole",
                table: "Quyen",
                columns: new[] { "idRole", "NameRole" });

            migrationBuilder.CreateIndex(
                name: "IX_ResfreshTokens_idUser",
                table: "ResfreshTokens",
                column: "idUser",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_IdRole",
                table: "UserInformation",
                column: "IdRole",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ResfreshTokens");

            migrationBuilder.DropTable(
                name: "UserInformation");

            migrationBuilder.DropTable(
                name: "Quyen");
        }
    }
}
