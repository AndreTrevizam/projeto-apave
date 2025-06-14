using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Usuario
{
  [Key]
  public int Id { get; set; }

  [Required(ErrorMessage = "O nome é obrigatório.")]
  public string Nome { get; set; }

  [EmailAddress, Required(ErrorMessage = "O Email é obrigatório.")]
  public string Email { get; set; }

  [Required(ErrorMessage = "A senha é obrigatória.")]
  [MinLength(6, ErrorMessage = "A senha deve ter no minimo 6 caracteres")] 
  public string Senha { get; set; }

  [NotMapped]
  [Compare("Senha", ErrorMessage = "As senhas não coincidem")]
  [Required(ErrorMessage = "A confirmação de senha é obrigatória.")]
  public string ConfirmarSenha { get; set; }

  // Campos especificos para cliente
  [RegularExpression(@"^\(?\d{2}\)?[\s-]?\d{4,5}-?\d{4}$",
    ErrorMessage = "Informe um telefone válido."), MaxLength(20)]
  public string? Telefone { get; set; }

  [NotMapped]
  public bool EhFuncionario { get; set; }

  // O default de criação de um usuário é do tipo Cliente
  [Required]
  public TipoUsuario Tipo { get; set; }
  public DateTime DataCriacao { get; set; } = DateTime.Now;
  public DateTime? DataAlteracao { get; set; }


}
public enum TipoUsuario
{
  Cliente,
  Funcionario
}