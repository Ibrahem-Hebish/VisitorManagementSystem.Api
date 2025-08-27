using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations.Shared
{
    /// <inheritdoc />
    public partial class SharedInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SharedTenants",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ConnectionString = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedTenants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SharedBranch",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Address_Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address_Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedBranch", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedBranch_SharedTenants_TenantId",
                        column: x => x.TenantId,
                        principalTable: "SharedTenants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharedUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    BranchId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedUser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedUser_SharedBranch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "SharedBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SharedUserToken",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    AccessTokenExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RefreshTokenExpiredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InUse = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SharedUserToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SharedUserToken_SharedBranch_BranchId",
                        column: x => x.BranchId,
                        principalTable: "SharedBranch",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SharedBranch_TenantId",
                table: "SharedBranch",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedUser_BranchId",
                table: "SharedUser",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_SharedUserToken_BranchId",
                table: "SharedUserToken",
                column: "BranchId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SharedUser");

            migrationBuilder.DropTable(
                name: "SharedUserToken");

            migrationBuilder.DropTable(
                name: "SharedBranch");

            migrationBuilder.DropTable(
                name: "SharedTenants");
        }
    }
}
