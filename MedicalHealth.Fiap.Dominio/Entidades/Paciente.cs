using MedicalHealth.Fiap.SharedKernel.Data;

namespace MedicalHealth.Fiap.Dominio.Entidades;

public class Paciente : EntidadeBase, IAggregateRoot
{
    public string? Nome { get; set; }
    public string? CPF { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }

    public Paciente() { }

    public Paciente(string? nome, string? cpf, string? email, string? senha)
    {
        Nome = nome;
        CPF = cpf;
        Email = email;
        Senha = senha;
    }

    public void AlterarPaciente(string? nome, string? cpf, string? email, string? senha)
    {
        Nome = nome;
        CPF = cpf;
        Email = email;
        Senha = senha;
        AtualizarDataAtualizacao();
    }

    public void ExcluirPaciente()
    {
        Excluir();
    }
}
