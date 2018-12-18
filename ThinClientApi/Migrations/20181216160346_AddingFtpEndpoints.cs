using Microsoft.EntityFrameworkCore.Migrations;

namespace ThinClientApi.Migrations
{
    public partial class AddingFtpEndpoints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FtpEndpoint",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    DomainId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FtpEndpoint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FtpEndpoint_Domains_DomainId",
                        column: x => x.DomainId,
                        principalTable: "Domains",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FtpEndpoint_DomainId",
                table: "FtpEndpoint",
                column: "DomainId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FtpEndpoint");
        }
    }
}
