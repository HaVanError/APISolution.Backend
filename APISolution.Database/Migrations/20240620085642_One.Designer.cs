﻿// <auto-generated />
using System;
using APISolution.Database.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace APISolution.Database.Migrations
{
    [DbContext(typeof(DdConnect))]
    [Migration("20240620085642_One")]
    partial class One
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("APISolution.Database.Entity.DichVu", b =>
                {
                    b.Property<int>("IdDichVu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdDichVu"));

                    b.Property<float>("Gia")
                        .HasColumnType("real");

                    b.Property<string>("NameDichVu")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.HasKey("IdDichVu");

                    b.HasIndex("IdDichVu", "NameDichVu");

                    b.ToTable("DichVu", (string)null);
                });

            modelBuilder.Entity("APISolution.Database.Entity.LoaiPhong", b =>
                {
                    b.Property<int>("IdLoaiPhong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdLoaiPhong"));

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IdLoaiPhong");

                    b.HasIndex("Name", "IdLoaiPhong")
                        .IsUnique();

                    b.ToTable("LoaiPhong", (string)null);
                });

            modelBuilder.Entity("APISolution.Database.Entity.PhieuDatPhong", b =>
                {
                    b.Property<int>("IdPhieuDatPhong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPhieuDatPhong"));

                    b.Property<string>("GiaPhong")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdPhong")
                        .HasColumnType("int");

                    b.Property<string>("SoDienThoai")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("TenNguoiDat")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TenPhong")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IdPhieuDatPhong");

                    b.HasIndex("IdPhong")
                        .IsUnique();

                    b.HasIndex("TenPhong", "IdPhieuDatPhong", "TenNguoiDat");

                    b.ToTable("PhieuDatPhong", (string)null);
                });

            modelBuilder.Entity("APISolution.Database.Entity.PhieuDichVu", b =>
                {
                    b.Property<int>("IdPhieuDichVu")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPhieuDichVu"));

                    b.Property<float>("Gia")
                        .HasColumnType("real");

                    b.Property<int>("IdDichVu")
                        .HasColumnType("int");

                    b.Property<int>("IdPhieuDatPhong")
                        .HasColumnType("int");

                    b.Property<int>("IdPhong")
                        .HasColumnType("int");

                    b.Property<int>("SoLuong")
                        .HasColumnType("int");

                    b.Property<string>("TenDichVu")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("TenKhachHang")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TenPhong")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ThanhTien")
                        .HasColumnType("float");

                    b.HasKey("IdPhieuDichVu");

                    b.HasIndex("IdDichVu");

                    b.HasIndex("IdPhieuDatPhong");

                    b.HasIndex("IdPhong");

                    b.HasIndex("TenDichVu", "IdPhieuDichVu");

                    b.ToTable("PhieuDatDichVu", (string)null);
                });

            modelBuilder.Entity("APISolution.Database.Entity.Phong", b =>
                {
                    b.Property<int>("IdPhong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdPhong"));

                    b.Property<string>("Describe")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("GiaPhong")
                        .HasColumnType("real");

                    b.Property<int>("IdLoaiPhong")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("StatusPhong")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.HasKey("IdPhong");

                    b.HasIndex("IdLoaiPhong");

                    b.HasIndex("IdPhong", "Name");

                    b.ToTable("Phong", (string)null);
                });

            modelBuilder.Entity("APISolution.Database.Entity.ResfreshToken", b =>
                {
                    b.Property<int>("IdToken")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdToken"));

                    b.Property<string>("Accesstoken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Resfreshtoken")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("idUser")
                        .HasColumnType("int");

                    b.HasKey("IdToken");

                    b.HasIndex("idUser")
                        .IsUnique();

                    b.ToTable("ResfreshTokens");
                });

            modelBuilder.Entity("APISolution.Database.Entity.Role", b =>
                {
                    b.Property<int>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdRole"));

                    b.Property<string>("MoTa")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NameRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("IdRole");

                    b.HasIndex("IdRole", "NameRole");

                    b.ToTable("Quyen", (string)null);
                });

            modelBuilder.Entity("APISolution.Database.Entity.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("200");

                    b.Property<string>("City")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("100");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdRole")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)")
                        .HasDefaultValue("50");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdRole");

                    b.HasIndex("Id", "Name");

                    b.ToTable("UserInformation", (string)null);
                });

            modelBuilder.Entity("APISolution.Database.Entity.PhieuDatPhong", b =>
                {
                    b.HasOne("APISolution.Database.Entity.Phong", "Phong")
                        .WithOne("PhieuDatPhongs")
                        .HasForeignKey("APISolution.Database.Entity.PhieuDatPhong", "IdPhong")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Phong");
                });

            modelBuilder.Entity("APISolution.Database.Entity.PhieuDichVu", b =>
                {
                    b.HasOne("APISolution.Database.Entity.DichVu", "DichVu")
                        .WithMany("PhieuDichVus")
                        .HasForeignKey("IdDichVu")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("APISolution.Database.Entity.PhieuDatPhong", "PhieuDatPhong")
                        .WithMany("PhieuDichVus")
                        .HasForeignKey("IdPhieuDatPhong")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("APISolution.Database.Entity.Phong", "Phong")
                        .WithMany("PhieuDichVus")
                        .HasForeignKey("IdPhong")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("DichVu");

                    b.Navigation("PhieuDatPhong");

                    b.Navigation("Phong");
                });

            modelBuilder.Entity("APISolution.Database.Entity.Phong", b =>
                {
                    b.HasOne("APISolution.Database.Entity.LoaiPhong", "LoaiPhongs")
                        .WithMany("Phong")
                        .HasForeignKey("IdLoaiPhong")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("LoaiPhongs");
                });

            modelBuilder.Entity("APISolution.Database.Entity.ResfreshToken", b =>
                {
                    b.HasOne("APISolution.Database.Entity.User", "User")
                        .WithOne("ResfreshToken")
                        .HasForeignKey("APISolution.Database.Entity.ResfreshToken", "idUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("APISolution.Database.Entity.User", b =>
                {
                    b.HasOne("APISolution.Database.Entity.Role", "Role")
                        .WithMany("User")
                        .HasForeignKey("IdRole")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("APISolution.Database.Entity.DichVu", b =>
                {
                    b.Navigation("PhieuDichVus");
                });

            modelBuilder.Entity("APISolution.Database.Entity.LoaiPhong", b =>
                {
                    b.Navigation("Phong");
                });

            modelBuilder.Entity("APISolution.Database.Entity.PhieuDatPhong", b =>
                {
                    b.Navigation("PhieuDichVus");
                });

            modelBuilder.Entity("APISolution.Database.Entity.Phong", b =>
                {
                    b.Navigation("PhieuDatPhongs")
                        .IsRequired();

                    b.Navigation("PhieuDichVus");
                });

            modelBuilder.Entity("APISolution.Database.Entity.Role", b =>
                {
                    b.Navigation("User");
                });

            modelBuilder.Entity("APISolution.Database.Entity.User", b =>
                {
                    b.Navigation("ResfreshToken")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}