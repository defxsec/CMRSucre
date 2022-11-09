using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMRSucre.Migrations
{
    public partial class ModificacionConfigs4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Utilidad",
                table: "Configs",
                type: "decimal(14,2)",
                precision: 14,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,1)",
                oldPrecision: 14,
                oldScale: 1);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Utilidad",
                table: "Configs",
                type: "decimal(14,1)",
                precision: 14,
                scale: 1,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(14,2)",
                oldPrecision: 14,
                oldScale: 2);
        }
    }
}
