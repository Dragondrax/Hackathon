using MedicalHealth.Fiap.SharedKernel.Data;

namespace MedicalHealth.Fiap.Dominio.Entidades;

public class Medico : EntidadeBase, IAggregateRoot
{
    public string? Nome { get; private set; }
    public string? CPF { get; private set; }
    public string? CRM { get; private set; }
    public string? Email { get; private set; }
    public string? Senha { get; private set; }

    public Medico() { }

    public Medico(string? nome, string? cpf, string? crm, string? email, string? senha)
    {
        Nome = nome;
        CPF = cpf;
        CRM = crm;
        Email = email;
        Senha = senha;
    }

    public void AlterarMedico(string nome, string cpf, string crm, string email, string senha)
    {
        Nome = nome;
        CPF = cpf;
        CRM = crm;
        Email = email;
        Senha = senha;
        AtualizarDataAtualizacao();
    }

    public void ExcluirMedico()
    {
        Excluir();
    }
}
