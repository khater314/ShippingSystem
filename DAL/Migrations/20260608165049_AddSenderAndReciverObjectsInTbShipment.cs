using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddSenderAndReciverObjectsInTbShipment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShipments_TbUserContacts",
                table: "TbShipments");

            migrationBuilder.CreateIndex(
                name: "IX_TbShipments_ReceiverId",
                table: "TbShipments",
                column: "ReceiverId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShipments_TbUserContacts_Receiver",
                table: "TbShipments",
                column: "ReceiverId",
                principalTable: "TbUserContacts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShipments_TbUserContacts_Sender",
                table: "TbShipments",
                column: "SenderId",
                principalTable: "TbUserContacts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbShipments_TbUserContacts_Receiver",
                table: "TbShipments");

            migrationBuilder.DropForeignKey(
                name: "FK_TbShipments_TbUserContacts_Sender",
                table: "TbShipments");

            migrationBuilder.DropIndex(
                name: "IX_TbShipments_ReceiverId",
                table: "TbShipments");

            migrationBuilder.AddForeignKey(
                name: "FK_TbShipments_TbUserContacts",
                table: "TbShipments",
                column: "SenderId",
                principalTable: "TbUserContacts",
                principalColumn: "Id");
        }
    }
}
