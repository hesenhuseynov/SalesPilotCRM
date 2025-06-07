using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalesPilotCRM.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class logicChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_CreatedByUserId",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_CreatedByUserId",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "Activities");

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Roles",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Notifications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "DealStages",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Deals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "CustomerStatuses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "ActivityTypes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Activities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CreatedById",
                table: "Activities",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_CreatedById",
                table: "Activities",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Activities_Users_CreatedById",
                table: "Activities");

            migrationBuilder.DropIndex(
                name: "IX_Activities_CreatedById",
                table: "Activities");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "DealStages");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Deals");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "CustomerStatuses");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "ActivityTypes");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Activities");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserId",
                table: "Activities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CreatedByUserId",
                table: "Activities",
                column: "CreatedByUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Activities_Users_CreatedByUserId",
                table: "Activities",
                column: "CreatedByUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
