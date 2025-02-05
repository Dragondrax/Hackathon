using FluentValidation;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;

namespace MedicalHealth.Fiap.Aplicacao.DTO
{
    public class RemoverAgendaMedicoRequestModel
    {
        public List<Guid> Id { get; set; }
    }

    public class RemoverAgendaMedicoRequestModelValidator : AbstractValidator<RemoverAgendaMedicoRequestModel>
    {
        public RemoverAgendaMedicoRequestModelValidator()
        {

            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage(MensagemAgenda.MENSAGEM_ID_NAO_PODE_SER_NULO_OU_VAZIO)
                .NotNull()
                .WithMessage(MensagemAgenda.MENSAGEM_ID_NAO_PODE_SER_NULO_OU_VAZIO);
        }
    }
}
