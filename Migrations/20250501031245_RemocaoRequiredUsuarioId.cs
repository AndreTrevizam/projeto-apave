using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace projeto_apave.Migrations
{
    /// <inheritdoc />
    public partial class RemocaoRequiredUsuarioId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Painel_Usuario_UsuarioId",
                table: "Painel");

            migrationBuilder.AddForeignKey(
                name: "FK_Painel_Usuario_UsuarioId",
                table: "Painel",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Painel_Usuario_UsuarioId",
                table: "Painel");

            migrationBuilder.AddForeignKey(
                name: "FK_Painel_Usuario_UsuarioId",
                table: "Painel",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
