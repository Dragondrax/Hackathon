using FluentValidation;
using MedicalHealth.Fiap.Infraestrutura.Enum;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;

namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class CriarAlteraUsuarioDTO
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Roles Role { get; set; }
    }

    public class CriarAlteraUsuarioDTOValidator : AbstractValidator<CriarAlteraUsuarioDTO>
    {
        public CriarAlteraUsuarioDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(MensagemUsuario.MENSAGEM_EMAIL_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemUsuario.MENSAGEM_EMAIL_NAO_PODE_SER_NULO)
                .EmailAddress()
                .WithMessage(MensagemUsuario.MENSAGEM_EMAIL_NAO_ESTA_NO_FORMATO_CORRETO);

            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(MensagemUsuario.MENSAGEM_NOME_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemUsuario.MENSAGEM_NOME_NAO_PODE_SER_NULO);

            RuleFor(x => x.Role)
                .NotNull()
                .WithMessage(MensagemUsuario.MENSAGEM_ROLE_NAO_PODE_SER_NULO);

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage(MensagemUsuario.MENSAGEM_SENHA_NAO_PODE_SER_VAZIO)
                .MinimumLength(10)
                .WithMessage(MensagemUsuario.MENSAGEM_SENHA_NAO_PODE_SER_MENOR_QUE_10_CARACTERES)
                .NotNull()
                .WithMessage(MensagemUsuario.MENSAGEM_SENHA_NAO_PODE_SER_NULO);
        }
    }
}
