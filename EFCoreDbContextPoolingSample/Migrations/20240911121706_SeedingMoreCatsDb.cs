using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCoreDbContextPoolingSample.Migrations
{
    /// <inheritdoc />
    public partial class SeedingMoreCatsDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("58a9b59d-b886-418c-a219-b71f1771218e"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("a21e78f4-4d14-4c1a-bbe7-42c85bbfd6b0"));

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Age", "Breed", "Nickname" },
                values: new object[,]
                {
                    { new Guid("26a0264b-3db1-4dbf-abe9-4e91b311f523"), 6, 1, "Desconhecido" },
                    { new Guid("33427e77-4ccc-4907-a81c-1522a4dffa28"), 8, 4, "Chorão" },
                    { new Guid("3af5723b-eb68-4039-b455-e537b8f9ded5"), 1, 3, "Desconhecido" },
                    { new Guid("4143777a-0ad5-404a-8af2-a6224fe2a3c2"), 6, 3, "Miau Safado" },
                    { new Guid("4ba47fd1-a2b7-4abb-9e97-d3ec979918c4"), 1, 0, "Desconhecido" },
                    { new Guid("58045a1f-6a49-4514-ac0e-5dc67b4e2b17"), 9, 3, "Briguento" },
                    { new Guid("6719e0e3-77cf-4328-831f-61af0e623a49"), 7, 3, "Ghraarrr" },
                    { new Guid("72f24f81-c90e-4e45-837e-58609f79d20d"), 4, 2, "Desconhecido" },
                    { new Guid("8b1fd40e-2472-43db-9082-2a27e0b043df"), 1, 4, "Desconhecido" },
                    { new Guid("ba5c26a2-bf46-4443-9902-346e98fbcc9f"), 4, 0, "Poof" },
                    { new Guid("c6eb2104-506a-45e9-9f71-5d2a5df4357a"), 12, 1, "Miau" },
                    { new Guid("d284d8c0-6fa0-401c-b239-48d736d1d584"), 2, 1, "Garfield" },
                    { new Guid("d3d5ea45-068b-4293-a962-1f1ad6a7a5fa"), 5, 4, "Bilu" },
                    { new Guid("f5166711-047a-40ba-b95d-33cd31965f65"), 1, 2, "Nanan" },
                    { new Guid("f86f7c36-e7bd-4b41-b397-8c60a64d0e86"), 4, 4, "Desconhecido" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("26a0264b-3db1-4dbf-abe9-4e91b311f523"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("33427e77-4ccc-4907-a81c-1522a4dffa28"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("3af5723b-eb68-4039-b455-e537b8f9ded5"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("4143777a-0ad5-404a-8af2-a6224fe2a3c2"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("4ba47fd1-a2b7-4abb-9e97-d3ec979918c4"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("58045a1f-6a49-4514-ac0e-5dc67b4e2b17"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("6719e0e3-77cf-4328-831f-61af0e623a49"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("72f24f81-c90e-4e45-837e-58609f79d20d"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("8b1fd40e-2472-43db-9082-2a27e0b043df"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("ba5c26a2-bf46-4443-9902-346e98fbcc9f"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("c6eb2104-506a-45e9-9f71-5d2a5df4357a"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("d284d8c0-6fa0-401c-b239-48d736d1d584"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("d3d5ea45-068b-4293-a962-1f1ad6a7a5fa"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("f5166711-047a-40ba-b95d-33cd31965f65"));

            migrationBuilder.DeleteData(
                table: "Cats",
                keyColumn: "Id",
                keyValue: new Guid("f86f7c36-e7bd-4b41-b397-8c60a64d0e86"));

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Age", "Breed", "Nickname" },
                values: new object[,]
                {
                    { new Guid("58a9b59d-b886-418c-a219-b71f1771218e"), 12, 1, "Miau" },
                    { new Guid("a21e78f4-4d14-4c1a-bbe7-42c85bbfd6b0"), 12, 1, "Garfield" }
                });
        }
    }
}
