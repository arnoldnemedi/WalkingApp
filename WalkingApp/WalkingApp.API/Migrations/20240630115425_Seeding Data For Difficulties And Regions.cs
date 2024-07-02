using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WalkingApp.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDataForDifficultiesAndRegions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("2f70e6f4-b46b-4a56-8504-73d06ae62940"), "Medium" },
                    { new Guid("7b849395-b5a3-4a38-9f44-616e5e5ddca2"), "Hard" },
                    { new Guid("8f878990-cdf0-47ca-93f2-e868c80f3a4b"), "Easy" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[] { new Guid("63169c6e-b896-43b2-9aab-a907c1127b7f"), "AKL", "Auckland", "" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("2f70e6f4-b46b-4a56-8504-73d06ae62940"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("7b849395-b5a3-4a38-9f44-616e5e5ddca2"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("8f878990-cdf0-47ca-93f2-e868c80f3a4b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("63169c6e-b896-43b2-9aab-a907c1127b7f"));
        }
    }
}
