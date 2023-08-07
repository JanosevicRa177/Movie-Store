using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieStore.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RefactoringValueObjectMappings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovieSubscription",
                table: "PurchasedMovies");

            migrationBuilder.DropColumn(
                name: "Subscription",
                table: "Customers");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpirationDate",
                table: "PurchasedMovies",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "PurchasedMovies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Customers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StatusExpirationDate",
                table: "Customers",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpirationDate",
                table: "PurchasedMovies");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "PurchasedMovies");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "StatusExpirationDate",
                table: "Customers");

            migrationBuilder.AddColumn<string>(
                name: "MovieSubscription",
                table: "PurchasedMovies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Subscription",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
