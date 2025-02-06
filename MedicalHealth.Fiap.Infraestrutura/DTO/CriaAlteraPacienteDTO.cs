using FluentValidation;
using MedicalHealth.Fiap.Infraestrutura.Enum;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;

namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class CriaAlteraPacienteDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
    }

    public class CriaAlteraPacienteDTOValidator : AbstractValidator<CriaAlteraPacienteDTO>
    {
        public CriaAlteraPacienteDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(MensagemPaciente.MENSAGEM_NOME_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemPaciente.MENSAGEM_NOME_NAO_PODE_SER_NULO);

            RuleFor(x => x.CPF)
                .NotEmpty()
                .WithMessage(MensagemPaciente.MENSAGEM_CPF_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemPaciente.MENSAGEM_CPF_NAO_PODE_SER_NULO);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(MensagemPaciente.MENSAGEM_EMAIL_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemPaciente.MENSAGEM_EMAIL_NAO_PODE_SER_NULO)
                .EmailAddress()
                .WithMessage(MensagemPaciente.MENSAGEM_EMAIL_NAO_ESTA_NO_FORMATO_CORRETO);
        }
    }
}
