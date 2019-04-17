using Microsoft.EntityFrameworkCore.Migrations;

namespace JobYub.Migrations
{
    public partial class removeID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementCompanyTypes_CompanyType_ID",
                table: "AdvertisementCompanyTypes");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisementCompanyTypes_ID",
                table: "AdvertisementCompanyTypes");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "AdvertisementCompanyTypes");

            migrationBuilder.AddColumn<int>(
                name: "CompanyTypeID",
                table: "AdvertisementCompanyTypes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementCompanyTypes_CompanyTypeID",
                table: "AdvertisementCompanyTypes",
                column: "CompanyTypeID");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementCompanyTypes_CompanyType_CompanyTypeID",
                table: "AdvertisementCompanyTypes",
                column: "CompanyTypeID",
                principalTable: "CompanyType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementCompanyTypes_CompanyType_CompanyTypeID",
                table: "AdvertisementCompanyTypes");

            migrationBuilder.DropIndex(
                name: "IX_AdvertisementCompanyTypes_CompanyTypeID",
                table: "AdvertisementCompanyTypes");

            migrationBuilder.DropColumn(
                name: "CompanyTypeID",
                table: "AdvertisementCompanyTypes");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "AdvertisementCompanyTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AdvertisementCompanyTypes_ID",
                table: "AdvertisementCompanyTypes",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementCompanyTypes_CompanyType_ID",
                table: "AdvertisementCompanyTypes",
                column: "ID",
                principalTable: "CompanyType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
