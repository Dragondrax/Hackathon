using FluentValidation;
using MedicalHealth.Fiap.SharedKernel.Mensagens;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;

namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class AceiteConsultaMedicoRequestModel
    {
        public Guid ConsultaId { get; set; }
        public bool Aceite {  get; set; }
    }

    public class AceiteConsultaMedicoRequestModelValidator : AbstractValidator<AceiteConsultaMedicoRequestModel>
    {
        public AceiteConsultaMedicoRequestModelValidator()
        {
            RuleFor(x => x.ConsultaId)
                .NotNull()
                .NotEmpty()
                .WithMessage(MensagemConsulta.MENSAGEM_CONSULTA_ID_NAO_PODE_SER_VAZIO);

            RuleFor(x => x.Aceite)
                .NotNull()
                .WithMessage(MensagemConsulta.MENSAGEM_ACEITE_NAO_PODE_SER_NULO);
        }
    }
}
