using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Talabat.Repository.Identity.Migrations
{
    /// <inheritdoc />
    public partial class IdentityUpdatingonAddressModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Addresses_addressId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_addressId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "addressId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Addresses",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_AppUserId",
                table: "Addresses",
                column: "AppUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_AspNetUsers_AppUserId",
                table: "Addresses",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_AspNetUsers_AppUserId",
                table: "Addresses");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_AppUserId",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Addresses");

            migrationBuilder.AddColumn<int>(
                name: "addressId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_addressId",
                table: "AspNetUsers",
                column: "addressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Addresses_addressId",
                table: "AspNetUsers",
                column: "addressId",
                principalTable: "Addresses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
