using Microsoft.EntityFrameworkCore.Migrations;

namespace JobYub.Migrations
{
    public partial class change_ads_cmpntype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdvertisementCompanyTypes",
                columns: table => new
                {
                    CompanyTypetID = table.Column<int>(nullable: false),
                    AdvertisementID = table.Column<int>(nullable: false),
                    ID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertisementCompanyTypes", x => new { x.AdvertisementID, x.CompanyTypetID });
                    table.ForeignKey(
                        name: "FK_AdvertisementCompanyTypes_Advertisement_AdvertisementID",
                        column: x => x.AdvertisementID,
                        principalTable: "Advertisement",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertisementCompanyTypes_CompanyType_ID",
                        column: x => x.ID,
                        principalTable: "CompanyType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementCompanyTypes_ID",
                table: "AdvertisementCompanyTypes",
                column: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertisementCompanyTypes");
        }
    }
}
