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
                name: "DichVu",
                columns: table => new
                {
                    IdDichVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameDichVu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DichVu", x => x.IdDichVu);
                });

            migrationBuilder.CreateTable(
                name: "LoaiPhong",
                columns: table => new
                {
                    IdLoaiPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiPhong", x => x.IdLoaiPhong);
                });

            migrationBuilder.CreateTable(
                name: "Quyen",
                columns: table => new
                {
                    IdRole = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameRole = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MoTa = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quyen", x => x.IdRole);
                });

            migrationBuilder.CreateTable(
                name: "Phong",
                columns: table => new
                {
                    IdPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Describe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdLoaiPhong = table.Column<int>(type: "int", nullable: false),
                    StatusPhong = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    GiaPhong = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phong", x => x.IdPhong);
                    table.ForeignKey(
                        name: "FK_Phong_LoaiPhong_IdLoaiPhong",
                        column: x => x.IdLoaiPhong,
                        principalTable: "LoaiPhong",
                        principalColumn: "IdLoaiPhong",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false, defaultValue: "50"),
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
                        principalColumn: "IdRole",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PhieuDatPhong",
                columns: table => new
                {
                    IdPhieuDatPhong = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhong = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    GiaPhong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenNguoiDat = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IdPhong = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuDatPhong", x => x.IdPhieuDatPhong);
                    table.ForeignKey(
                        name: "FK_PhieuDatPhong_Phong_IdPhong",
                        column: x => x.IdPhong,
                        principalTable: "Phong",
                        principalColumn: "IdPhong",
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhieuDatDichVu",
                columns: table => new
                {
                    IdPhieuDichVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenKhachHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TenDichVu = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IdPhong = table.Column<int>(type: "int", nullable: false),
                    IdDichVu = table.Column<int>(type: "int", nullable: false),
                    IdPhieuDatPhong = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<float>(type: "real", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuDatDichVu", x => x.IdPhieuDichVu);
                    table.ForeignKey(
                        name: "FK_PhieuDatDichVu_DichVu_IdDichVu",
                        column: x => x.IdDichVu,
                        principalTable: "DichVu",
                        principalColumn: "IdDichVu",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhieuDatDichVu_PhieuDatPhong_IdPhieuDatPhong",
                        column: x => x.IdPhieuDatPhong,
                        principalTable: "PhieuDatPhong",
                        principalColumn: "IdPhieuDatPhong",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PhieuDatDichVu_Phong_IdPhong",
                        column: x => x.IdPhong,
                        principalTable: "Phong",
                        principalColumn: "IdPhong",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DichVu_IdDichVu_NameDichVu",
                table: "DichVu",
                columns: new[] { "IdDichVu", "NameDichVu" });

            migrationBuilder.CreateIndex(
                name: "IX_LoaiPhong_Name_IdLoaiPhong",
                table: "LoaiPhong",
                columns: new[] { "Name", "IdLoaiPhong" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDatDichVu_IdDichVu",
                table: "PhieuDatDichVu",
                column: "IdDichVu");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDatDichVu_IdPhieuDatPhong",
                table: "PhieuDatDichVu",
                column: "IdPhieuDatPhong");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDatDichVu_IdPhong",
                table: "PhieuDatDichVu",
                column: "IdPhong");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDatDichVu_TenDichVu_IdPhieuDichVu",
                table: "PhieuDatDichVu",
                columns: new[] { "TenDichVu", "IdPhieuDichVu" });

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDatPhong_IdPhong",
                table: "PhieuDatPhong",
                column: "IdPhong",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhieuDatPhong_TenPhong_IdPhieuDatPhong_TenNguoiDat",
                table: "PhieuDatPhong",
                columns: new[] { "TenPhong", "IdPhieuDatPhong", "TenNguoiDat" });

            migrationBuilder.CreateIndex(
                name: "IX_Phong_IdLoaiPhong",
                table: "Phong",
                column: "IdLoaiPhong");

            migrationBuilder.CreateIndex(
                name: "IX_Phong_IdPhong_Name",
                table: "Phong",
                columns: new[] { "IdPhong", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_Quyen_IdRole_NameRole",
                table: "Quyen",
                columns: new[] { "IdRole", "NameRole" });

            migrationBuilder.CreateIndex(
                name: "IX_ResfreshTokens_idUser",
                table: "ResfreshTokens",
                column: "idUser",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_Id_Name",
                table: "UserInformation",
                columns: new[] { "Id", "Name" });

            migrationBuilder.CreateIndex(
                name: "IX_UserInformation_IdRole",
                table: "UserInformation",
                column: "IdRole");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PhieuDatDichVu");

            migrationBuilder.DropTable(
                name: "ResfreshTokens");

            migrationBuilder.DropTable(
                name: "DichVu");

            migrationBuilder.DropTable(
                name: "PhieuDatPhong");

            migrationBuilder.DropTable(
                name: "UserInformation");

            migrationBuilder.DropTable(
                name: "Phong");

            migrationBuilder.DropTable(
                name: "Quyen");

            migrationBuilder.DropTable(
                name: "LoaiPhong");
        }
    }
}
