using FluentValidation;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;

namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class AutenticarUsuarioDTO
    {
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class AutenticarUsuarioDTOValidator : AbstractValidator<AutenticarUsuarioDTO>
    {
        public AutenticarUsuarioDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(MensagemUsuario .MENSAGEM_EMAIL_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemUsuario.MENSAGEM_EMAIL_NAO_PODE_SER_VAZIO)
                .EmailAddress()
                .WithMessage(MensagemUsuario.MENSAGEM_EMAIL_NAO_ESTA_NO_FORMATO_CORRETO);

            RuleFor(x => x.Senha)
                .NotEmpty()
                .WithMessage(MensagemUsuario.MENSAGEM_SENHA_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemUsuario.MENSAGEM_SENHA_NAO_PODE_SER_NULO);
        }
    }
}
