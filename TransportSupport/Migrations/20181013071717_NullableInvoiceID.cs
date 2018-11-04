using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TransportSupport.Migrations
{
    public partial class NullableInvoiceID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreightInvoices_Invoices_InvoiceId",
                table: "FreightInvoices");

            migrationBuilder.AlterColumn<string>(
                name: "UnitOfMeasure",
                table: "Tracks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tracks",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "FreightInvoices",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_FreightInvoices_Invoices_InvoiceId",
                table: "FreightInvoices",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FreightInvoices_Invoices_InvoiceId",
                table: "FreightInvoices");

            migrationBuilder.AlterColumn<string>(
                name: "UnitOfMeasure",
                table: "Tracks",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tracks",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<int>(
                name: "InvoiceId",
                table: "FreightInvoices",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_FreightInvoices_Invoices_InvoiceId",
                table: "FreightInvoices",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
