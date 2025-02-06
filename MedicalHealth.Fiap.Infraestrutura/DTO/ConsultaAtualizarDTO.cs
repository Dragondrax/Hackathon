using FluentValidation;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;

namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class ConsultaAtualizarDTO
    {
        public Guid ConsultaId { get; set; }
        public string Justificativa {  get; set; }
        
        public ConsultaAtualizarDTO() { }
    }

    public class ConsultaAtualizarDTOValidator : AbstractValidator<ConsultaAtualizarDTO>
    {
        public ConsultaAtualizarDTOValidator()
        {
            RuleFor(x => x.ConsultaId)
                .NotEmpty()
                .WithMessage(MensagemAgenda.MENSAGEM_CONSULTA_NAO_PODE_SER_VAZIA)
                .NotNull()
                .WithMessage(MensagemAgenda.MENSAGEM_CONSULTA_NAO_PODE_SER_NULA);

            RuleFor(x => x.Justificativa)
               .NotEmpty()
               .WithMessage(MensagemAgenda.MENSAGEM_AGENDA_NAO_PODE_SER_VAZIA)
               .NotNull()
               .WithMessage(MensagemAgenda.MENSAGEM_AGENDA_NAO_PODE_SER_NULA);

        }
    }
}
