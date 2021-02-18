using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListingsApi.Migrations
{
    public partial class SeedingData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "SCO" },
                values: new object[] { 1, "Ethiopia", "ETH" });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "SCO" },
                values: new object[] { 2, "Caymen Iasland", "CI   " });

            migrationBuilder.InsertData(
                table: "Countries",
                columns: new[] { "Id", "Name", "SCO" },
                values: new object[] { 3, "Bahamas", "BS" });

            migrationBuilder.InsertData(
                table: "Hotel",
                columns: new[] { "Id", "Address", "CountryId", "Name", "Rating" },
                values: new object[] { 2, "Addis Ababa", 1, "Harmony Hotel", 4.2000000000000002 });

            migrationBuilder.InsertData(
                table: "Hotel",
                columns: new[] { "Id", "Address", "CountryId", "Name", "Rating" },
                values: new object[] { 1, "Negril", 2, "Sandals Resort and Spa", 4.5 });

            migrationBuilder.InsertData(
                table: "Hotel",
                columns: new[] { "Id", "Address", "CountryId", "Name", "Rating" },
                values: new object[] { 3, "George Town", 3, "Comfort Suites", 4.7000000000000002 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Hotel",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Countries",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
