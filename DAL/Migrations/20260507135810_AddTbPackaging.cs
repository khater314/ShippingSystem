using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddTbPackaging : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefaultAddress",
                table: "TbUserContacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "OtherAddressInfo",
                table: "TbUserContacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "TbUserContacts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DelivryDate",
                table: "TbShipments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(2000, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "PackagingId",
                table: "TbShipments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TbPackagings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())")
                        .Annotation("Relational:DefaultConstraintName", "DF_TbPackagings_Id"),
                    PackagingEName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PackagingAName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CurrentState = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbPackagings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbShipments_PackagingId",
                table: "TbShipments",
                column: "PackagingId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShipments_TbPackagings",
                table: "TbShipments",
                column: "PackagingId",
                principalTable: "TbPackagings",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShipments_TbPackagings",
                table: "TbShipments");

            migrationBuilder.DropTable(
                name: "TbPackagings");

            migrationBuilder.DropIndex(
                name: "IX_TbShipments_PackagingId",
                table: "TbShipments");

            migrationBuilder.DropColumn(
                name: "IsDefaultAddress",
                table: "TbUserContacts");

            migrationBuilder.DropColumn(
                name: "OtherAddressInfo",
                table: "TbUserContacts");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "TbUserContacts");

            migrationBuilder.DropColumn(
                name: "DelivryDate",
                table: "TbShipments");

            migrationBuilder.DropColumn(
                name: "PackagingId",
                table: "TbShipments");
        }
    }
}
