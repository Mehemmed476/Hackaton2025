using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem.DL.Migrations
{
    /// <inheritdoc />
    public partial class AddPictureToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RoomImageURL",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "1f9c48c3-55a9-4a41-aa76-4a440631c7ab", null, "User", "USER" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c10c9801-9957-4018-8e48-0c7812d47b50",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5ef4a5ce-eabd-4346-a3e1-3ed552c63fb6", "AQAAAAIAAYagAAAAEGGEdRjv5QrDgUfHZToPm2gHYMCSSsrLrayHQrgaHMjKhorM3J5uIKz9mbQu1rck4A==", "046b9cda-9f9e-44f5-94ad-29ccc4cbce3d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1f9c48c3-55a9-4a41-aa76-4a440631c7ab");

            migrationBuilder.DropColumn(
                name: "RoomImageURL",
                table: "Rooms");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c10c9801-9957-4018-8e48-0c7812d47b50",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b6f64bc-d92a-4534-9c77-174cb5cb5c1e", "AQAAAAIAAYagAAAAEHZNa0r8E9XyeXqiCGIPsg1e8ActxbqysL18mo4Ty3a4AAVDvCyWjvHZxgUCgIwq6A==", "95611cd8-4f06-423e-ac12-699bb2590fd8" });
        }
    }
}
