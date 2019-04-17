using Microsoft.EntityFrameworkCore.Migrations;

namespace JobYub.Migrations
{
    public partial class correcCoNae : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementCompanyTypes_CompanyType_CompanyTypeID",
                table: "AdvertisementCompanyTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvertisementCompanyTypes",
                table: "AdvertisementCompanyTypes");

            migrationBuilder.DropColumn(
                name: "CompanyTypetID",
                table: "AdvertisementCompanyTypes");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyTypeID",
                table: "AdvertisementCompanyTypes",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvertisementCompanyTypes",
                table: "AdvertisementCompanyTypes",
                columns: new[] { "AdvertisementID", "CompanyTypeID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementCompanyTypes_CompanyType_CompanyTypeID",
                table: "AdvertisementCompanyTypes",
                column: "CompanyTypeID",
                principalTable: "CompanyType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertisementCompanyTypes_CompanyType_CompanyTypeID",
                table: "AdvertisementCompanyTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AdvertisementCompanyTypes",
                table: "AdvertisementCompanyTypes");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyTypeID",
                table: "AdvertisementCompanyTypes",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "CompanyTypetID",
                table: "AdvertisementCompanyTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AdvertisementCompanyTypes",
                table: "AdvertisementCompanyTypes",
                columns: new[] { "AdvertisementID", "CompanyTypetID" });

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertisementCompanyTypes_CompanyType_CompanyTypeID",
                table: "AdvertisementCompanyTypes",
                column: "CompanyTypeID",
                principalTable: "CompanyType",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
