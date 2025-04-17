using System.ComponentModel.DataAnnotations;

public class Usuario
{
  [Key]
  public int Id { get; set; }

  [Required]
  public string Nome { get; set; }

  [EmailAddress, Required]
  public string Email { get; set; }

  [MinLength(6), Required]
  public string Senha { get; set; }

  // Campos especificos para cliente
  [StringLength(20)]
  public string? Telefone { get; set; }

  // O default de criação de um usuário é do tipo Cliente
  [Required]
  public TipoUsuario Tipo { get; set; } = TipoUsuario.Cliente;
  public DateTime DataCriacao { get; set; } = DateTime.Now;
  public DateTime DataAlteracao { get; set; } = DateTime.Now;


  public enum TipoUsuario {
    Cliente,
    Funcionario
  }
}