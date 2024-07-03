using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

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
                    GiaPhong = table.Column<float>(type: "real", nullable: false),
                    TenNguoiDat = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IdPhong = table.Column<int>(type: "int", nullable: false),
                    NgayDatPhong = table.Column<DateTime>(type: "datetime2", nullable: false)
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
                    IdDichVu = table.Column<int>(type: "int", nullable: false),
                    IdPhieuDatPhong = table.Column<int>(type: "int", nullable: false),
                    Gia = table.Column<float>(type: "real", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: false),
                    NgayDatDichVu = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhongIdPhong = table.Column<int>(type: "int", nullable: true)
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
                        name: "FK_PhieuDatDichVu_Phong_PhongIdPhong",
                        column: x => x.PhongIdPhong,
                        principalTable: "Phong",
                        principalColumn: "IdPhong");
                });

            migrationBuilder.CreateTable(
                name: "ThanhToan",
                columns: table => new
                {
                    IdThanhToan = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenPhong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TenKhachHang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TongThanhTien = table.Column<double>(type: "float", nullable: false),
                    TrangThaiThanhToan = table.Column<int>(type: "int", nullable: false, defaultValue: 2),
                    idPhieuDatPhong = table.Column<int>(type: "int", nullable: true),
                    NgayThanhToan = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTraPhong = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToan", x => x.IdThanhToan);
                    table.ForeignKey(
                        name: "FK_ThanhToan_PhieuDatPhong_idPhieuDatPhong",
                        column: x => x.idPhieuDatPhong,
                        principalTable: "PhieuDatPhong",
                        principalColumn: "IdPhieuDatPhong",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ThanhToan_PhieuDatPhong",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPhieuDatPhong = table.Column<int>(type: "int", nullable: false),
                    PhieuDatPhongIdPhieuDatPhong = table.Column<int>(type: "int", nullable: false),
                    IdThanhToan = table.Column<int>(type: "int", nullable: false),
                    ThanhToanIdThanhToan = table.Column<int>(type: "int", nullable: false),
                    NgayDat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NgayTra = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThanhToan_PhieuDatPhong", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThanhToan_PhieuDatPhong_PhieuDatPhong_PhieuDatPhongIdPhieuDatPhong",
                        column: x => x.PhieuDatPhongIdPhieuDatPhong,
                        principalTable: "PhieuDatPhong",
                        principalColumn: "IdPhieuDatPhong",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ThanhToan_PhieuDatPhong_ThanhToan_ThanhToanIdThanhToan",
                        column: x => x.ThanhToanIdThanhToan,
                        principalTable: "ThanhToan",
                        principalColumn: "IdThanhToan",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DichVu",
                columns: new[] { "IdDichVu", "Gia", "NameDichVu", "SoLuong" },
                values: new object[,]
                {
                    { 1, 15000f, "Sting", 1000 },
                    { 2, 15000f, "Bò hút", 1000 },
                    { 3, 18000f, "Bia Sài Gòn", 1000 },
                    { 4, 22000f, "Thuốc lá Hero", 1000 }
                });

            migrationBuilder.InsertData(
                table: "LoaiPhong",
                columns: new[] { "IdLoaiPhong", "MoTa", "Name" },
                values: new object[,]
                {
                    { 1, "Loại phòng VIP", "Vip" },
                    { 2, "Loại phòng Thường", "Thường" }
                });

            migrationBuilder.InsertData(
                table: "Quyen",
                columns: new[] { "IdRole", "MoTa", "NameRole" },
                values: new object[,]
                {
                    { 1, "Người quản trị hệ thống", "Admin" },
                    { 2, "Người dùng hệ thống", "User" }
                });

            migrationBuilder.InsertData(
                table: "Phong",
                columns: new[] { "IdPhong", "Describe", "GiaPhong", "IdLoaiPhong", "Name" },
                values: new object[,]
                {
                    { 1, "Phòng Vip A1", 200000f, 1, "A1" },
                    { 2, "Phòng Vip A2", 200000f, 1, "A2" },
                    { 3, "Phòng Vip A3", 200000f, 1, "A3" },
                    { 4, "Phòng Vip B1", 150000f, 2, "B1" },
                    { 5, "Phòng Vip B2", 150000f, 2, "B2" }
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
                name: "IX_PhieuDatDichVu_PhongIdPhong",
                table: "PhieuDatDichVu",
                column: "PhongIdPhong");

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
                name: "IX_PhieuDatPhong_TenPhong_TenNguoiDat",
                table: "PhieuDatPhong",
                columns: new[] { "TenPhong", "TenNguoiDat" });

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
                name: "IX_ThanhToan_idPhieuDatPhong",
                table: "ThanhToan",
                column: "idPhieuDatPhong");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_PhieuDatPhong_PhieuDatPhongIdPhieuDatPhong",
                table: "ThanhToan_PhieuDatPhong",
                column: "PhieuDatPhongIdPhieuDatPhong");

            migrationBuilder.CreateIndex(
                name: "IX_ThanhToan_PhieuDatPhong_ThanhToanIdThanhToan",
                table: "ThanhToan_PhieuDatPhong",
                column: "ThanhToanIdThanhToan");

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
                name: "ThanhToan_PhieuDatPhong");

            migrationBuilder.DropTable(
                name: "DichVu");

            migrationBuilder.DropTable(
                name: "UserInformation");

            migrationBuilder.DropTable(
                name: "ThanhToan");

            migrationBuilder.DropTable(
                name: "Quyen");

            migrationBuilder.DropTable(
                name: "PhieuDatPhong");

            migrationBuilder.DropTable(
                name: "Phong");

            migrationBuilder.DropTable(
                name: "LoaiPhong");
        }
    }
}
