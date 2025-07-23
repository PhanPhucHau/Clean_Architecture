using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CleanArchitechtureDemo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNotifyForeignKeys : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Users_UserId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Filters_Devices_DeviceId",
                table: "Filters");

            migrationBuilder.DropColumn(
                name: "DeviceNotifyId",
                table: "Notifies");

            migrationBuilder.DropColumn(
                name: "FilterNotifyId",
                table: "Notifies");

            migrationBuilder.DropColumn(
                name: "UserNotifyId",
                table: "Notifies");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "Filters",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Devices",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Users_UserId",
                table: "Devices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Filters_Devices_DeviceId",
                table: "Filters",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Devices_Users_UserId",
                table: "Devices");

            migrationBuilder.DropForeignKey(
                name: "FK_Filters_Devices_DeviceId",
                table: "Filters");

            migrationBuilder.AlterColumn<int>(
                name: "DeviceId",
                table: "Filters",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Devices",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Devices_Users_UserId",
                table: "Devices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Filters_Devices_DeviceId",
                table: "Filters",
                column: "DeviceId",
                principalTable: "Devices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
