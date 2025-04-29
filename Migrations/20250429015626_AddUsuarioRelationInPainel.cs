using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projeto_apave.Migrations
{
    /// <inheritdoc />
    public partial class AddUsuarioRelationInPainel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Painel",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Painel_UsuarioId",
                table: "Painel",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Painel_Usuario_UsuarioId",
                table: "Painel",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Painel_Usuario_UsuarioId",
                table: "Painel");

            migrationBuilder.DropIndex(
                name: "IX_Painel_UsuarioId",
                table: "Painel");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Painel");
        }
    }
}
