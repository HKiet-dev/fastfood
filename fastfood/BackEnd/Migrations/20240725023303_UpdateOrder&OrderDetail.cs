using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderOrderDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Orders",
                newName: "note");

            migrationBuilder.RenameColumn(
                name: "PhoneNo",
                table: "Orders",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "DateReceived",
                table: "Orders",
                newName: "ReceviedDate");

            migrationBuilder.RenameColumn(
                name: "CreateAt",
                table: "Orders",
                newName: "OrderDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Orders",
                newName: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "note",
                table: "Orders",
                newName: "Note");

            migrationBuilder.RenameColumn(
                name: "ReceviedDate",
                table: "Orders",
                newName: "DateReceived");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "Orders",
                newName: "PhoneNo");

            migrationBuilder.RenameColumn(
                name: "OrderDate",
                table: "Orders",
                newName: "CreateAt");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "Id");
        }
    }
}
