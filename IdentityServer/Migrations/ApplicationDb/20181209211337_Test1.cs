using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer.Migrations.ApplicationDb
{
    public partial class Test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Domain_DomainId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "RdpEndpoints");

            migrationBuilder.DropTable(
                name: "Domain");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DomainId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DomainId",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DomainId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Domain",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domain", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RdpEndpoints",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    DomainId = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RdpEndpoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RdpEndpoints_Domain_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domain",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DomainId",
                table: "AspNetUsers",
                column: "DomainId");

            migrationBuilder.CreateIndex(
                name: "IX_RdpEndpoints_DomainId",
                table: "RdpEndpoints",
                column: "DomainId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Domain_DomainId",
                table: "AspNetUsers",
                column: "DomainId",
                principalTable: "Domain",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
