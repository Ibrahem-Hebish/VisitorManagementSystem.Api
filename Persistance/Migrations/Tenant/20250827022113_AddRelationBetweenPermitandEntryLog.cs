using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations.Tenant
{
    /// <inheritdoc />
    public partial class AddRelationBetweenPermitandEntryLog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PermitId",
                table: "Entrylog",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Entrylog_PermitId",
                table: "Entrylog",
                column: "PermitId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Entrylog_Permit_PermitId",
                table: "Entrylog",
                column: "PermitId",
                principalTable: "Permit",
                principalColumn: "PermitId",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Entrylog_Permit_PermitId",
                table: "Entrylog");

            migrationBuilder.DropIndex(
                name: "IX_Entrylog_PermitId",
                table: "Entrylog");

            migrationBuilder.DropColumn(
                name: "PermitId",
                table: "Entrylog");
        }
    }
}
