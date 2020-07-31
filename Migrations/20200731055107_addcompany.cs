using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Routines.Api.Migrations
{
    public partial class addcompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df59237100"), "China", "ECommerce", "From Shenzhen", "Tencent", "Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716400"), "China", "Internet", "From Beijing", "Baidu", "Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542800"), "USA", "Software", "Photoshop?", "Adobe", "Software" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df59237111"), "USA", "Technology", "Wow", "SpaceX", "Rocket" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716411"), "Italy", "Football", "Football Club", "AC Milan", "Football Match" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542811"), "China", "ECommerce", "From Jiangsu", "Suning", "Goods" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df59237122"), "USA", "Internet", "Blocked", "Twitter", "Tweets" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716422"), "USA", "Internet", "Blocked", "Youtube", "Videos" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542822"), "China", "Security", "- -", "360", "Security Product" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df59237133"), "China", "ECommerce", "Brothers", "Jingdong", "Goods" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716433"), "China", "Internet", "Music?", "NetEase", "Songs" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542833"), "USA", "ECommerce", "Store", "Amazon", "Books" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("bbdee09c-089b-4d30-bece-44df59237144"), "USA", "Internet", "Not Exists?", "AOL", "Website" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("6fb600c1-9011-4fd7-9234-881379716444"), "USA", "Internet", "Who?", "Yahoo", "Mail" });

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Country", "Industry", "Introduction", "Name", "Product" },
                values: new object[] { new Guid("5efc910b-2f45-43df-afae-620d40542844"), "USA", "Internet", "Is it a company?", "Firefox", "Browser" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5efc910b-2f45-43df-afae-620d40542800"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5efc910b-2f45-43df-afae-620d40542811"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5efc910b-2f45-43df-afae-620d40542822"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5efc910b-2f45-43df-afae-620d40542833"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("5efc910b-2f45-43df-afae-620d40542844"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("6fb600c1-9011-4fd7-9234-881379716400"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("6fb600c1-9011-4fd7-9234-881379716411"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("6fb600c1-9011-4fd7-9234-881379716422"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("6fb600c1-9011-4fd7-9234-881379716433"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("6fb600c1-9011-4fd7-9234-881379716444"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("bbdee09c-089b-4d30-bece-44df59237100"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("bbdee09c-089b-4d30-bece-44df59237111"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("bbdee09c-089b-4d30-bece-44df59237122"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("bbdee09c-089b-4d30-bece-44df59237133"));

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: new Guid("bbdee09c-089b-4d30-bece-44df59237144"));
        }
    }
}
