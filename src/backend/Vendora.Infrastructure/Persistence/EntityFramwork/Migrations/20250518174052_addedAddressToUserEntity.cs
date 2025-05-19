using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Vendora.Infrastructure.Persistence.EntityFramwork.Migrations
{
    /// <inheritdoc />
    public partial class addedAddressToUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateOfExpiry",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "DateOfIssue",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JShShIR",
                table: "Users",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateOfExpiry",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "DateOfIssue",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "JShShIR",
                table: "Users");
        }
    }
}
