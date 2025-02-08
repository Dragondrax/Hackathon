using FluentValidation;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;

namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class BuscarEmailDTO
    {
        public string Email { get; set; }

        public BuscarEmailDTO() { }

        public BuscarEmailDTO(string email)
        {
            Email = email;
        }
    }

    public class BuscarEmailDTOValidator : AbstractValidator<BuscarEmailDTO>
    {
        public BuscarEmailDTOValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(MensagemUsuario.MENSAGEM_EMAIL_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemUsuario.MENSAGEM_EMAIL_NAO_PODE_SER_NULO)
                .EmailAddress()
                .WithMessage(MensagemUsuario.MENSAGEM_EMAIL_NAO_ESTA_NO_FORMATO_CORRETO);
        }
    }
}
