using System.ComponentModel.DataAnnotations;

public class Usuario
{
  [Key]
  public int Id { get; set; }

  [Required(ErrorMessage = "O nome é obrigatório.")]
  public string Nome { get; set; }

  [EmailAddress, Required(ErrorMessage = "O Email é obrigatório.")]
  public string Email { get; set; }

  [MinLength(6), Required(ErrorMessage = "A senha é obrigatória.")]
  public string Senha { get; set; }

  // Campos especificos para cliente
  [RegularExpression(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$", 
    ErrorMessage = "Informe um telefone válido."), MaxLength(20)]
  public string? Telefone { get; set; }

  // O default de criação de um usuário é do tipo Cliente
  [Required]
  public TipoUsuario Tipo { get; set; } = TipoUsuario.Cliente;
  public DateTime DataCriacao { get; set; } = DateTime.Now;
  public DateTime? DataAlteracao { get; set; }


  public enum TipoUsuario {
    Cliente,
    Funcionario
  }
}