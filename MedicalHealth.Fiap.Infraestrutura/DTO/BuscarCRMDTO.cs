using FluentValidation;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;

namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class BuscarCRMDTO
    {
        public string Crm {  get; set; }

        public BuscarCRMDTO() { }

        public BuscarCRMDTO(string crm)
        {
            Crm = crm;
        }
    }

    public class BuscarCRMDTOValidator : AbstractValidator<BuscarCRMDTO>
    {
        public BuscarCRMDTOValidator()
        {
            RuleFor(x => x.Crm)
                .NotEmpty()
                .WithMessage(MensagemMedico.CRM_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemMedico.CRM_NAO_PODE_SER_NULO)
                .Matches(@"^\d{4,6}-[A-Z]{2}$")
                .WithMessage(MensagemMedico.CRM_NAO_ESTA_NO_FORMATO_CORRETO);
        }
    }

}
