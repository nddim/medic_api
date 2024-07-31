using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace medic_api.Migrations
{
    /// <inheritdoc />
    public partial class PopravkaImena : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Stuatus",
                table: "UserProfile",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "UserProfile",
                newName: "Stuatus");
        }
    }
}
