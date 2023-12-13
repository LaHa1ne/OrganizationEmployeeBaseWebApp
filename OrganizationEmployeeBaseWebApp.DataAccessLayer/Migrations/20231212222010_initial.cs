using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OrganizationEmployeeBaseWebApp.DataAccessLayer.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    INN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LegalAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActualAddress = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizations", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    PassportSeries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_employees_organizations_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "organizations",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "organizations",
                columns: new[] { "OrganizationId", "ActualAddress", "INN", "LegalAddress", "Name" },
                values: new object[] { 1, "Фактический адрес 1", "1234567890", "Юридический адрес 1", "Организация 1" });

            migrationBuilder.InsertData(
                table: "organizations",
                columns: new[] { "OrganizationId", "ActualAddress", "INN", "LegalAddress", "Name" },
                values: new object[] { 2, "Фактический адрес 2", "0987654321", "Юридический адрес 2", "Организация 2" });

            migrationBuilder.InsertData(
                table: "organizations",
                columns: new[] { "OrganizationId", "ActualAddress", "INN", "LegalAddress", "Name" },
                values: new object[] { 3, "Фактический адрес 3", "1234554321", "Юридический адрес 3", "Организация 3" });

            migrationBuilder.InsertData(
                table: "employees",
                columns: new[] { "EmployeeId", "DateOfBirth", "FirstName", "LastName", "OrganizationId", "PassportNumber", "PassportSeries", "Patronymic" },
                values: new object[,]
                {
                    { 1, new DateOnly(2003, 7, 8), "Ксения", "Мельникова", 1, "123456", "12345678", "Васильевна" },
                    { 2, new DateOnly(1962, 12, 11), "Максим", "Пименов", 2, "164352", "42156478", "Евгеньевич" },
                    { 3, new DateOnly(1994, 8, 26), "Елизавета", "Миронова", 2, "956431", "86541235", "Елизавета" },
                    { 4, new DateOnly(1986, 3, 2), "Валерий", "Васильев", 2, "154356", "84569721", "Валентинович" },
                    { 5, new DateOnly(1975, 11, 1), "София", "Иванова", 1, "864352", "13945125", "Ивановна" },
                    { 6, new DateOnly(1986, 1, 30), "Игорь", "Девин", 2, "124553", "63541235", "Владимирович" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_employees_OrganizationId",
                table: "employees",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "employees");

            migrationBuilder.DropTable(
                name: "organizations");
        }
    }
}
