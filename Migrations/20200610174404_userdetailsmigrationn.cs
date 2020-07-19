using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DevSpace_API.Migrations
{
    public partial class userdetailsmigrationn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bio",
                table: "UserDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EmailId",
                table: "UserDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ExternalWebsite",
                table: "UserDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "JoinedOn",
                table: "UserDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "UserDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MediumLink",
                table: "UserDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "UserDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bio",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "ExternalWebsite",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "JoinedOn",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "MediumLink",
                table: "UserDetails");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "UserDetails");
        }
    }
}
