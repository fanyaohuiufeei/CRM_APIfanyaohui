﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BT.Data.Migrations
{
    public partial class CustomerSegmentations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerSegmentationID",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CustomerSegmentationName",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeesID",
                table: "Customers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomerSegmentations",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDel = table.Column<int>(nullable: false),
                    CreateID = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerSegmentations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Transforms_Customers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsDel = table.Column<int>(nullable: false),
                    CreateID = table.Column<int>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    FromEmployeesID = table.Column<int>(nullable: false),
                    ToEmployeesID = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transforms_Customers", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerSegmentations");

            migrationBuilder.DropTable(
                name: "Transforms_Customers");

            migrationBuilder.DropColumn(
                name: "CustomerSegmentationID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerSegmentationName",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "EmployeesID",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Customers");
        }
    }
}
