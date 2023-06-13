using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_API.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTabla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImangeUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle de la villa...", new DateTime(2023, 6, 13, 8, 23, 37, 225, DateTimeKind.Local).AddTicks(6691), new DateTime(2023, 6, 13, 8, 23, 37, 225, DateTimeKind.Local).AddTicks(6677), "", 100, "Villa Real", 4, 150.0 },
                    { 2, "", "Detalle de la en el fondo...", new DateTime(2023, 6, 13, 8, 23, 37, 225, DateTimeKind.Local).AddTicks(6694), new DateTime(2023, 6, 13, 8, 23, 37, 225, DateTimeKind.Local).AddTicks(6693), "", 60, "Villa Vista Plana", 2, 86.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
