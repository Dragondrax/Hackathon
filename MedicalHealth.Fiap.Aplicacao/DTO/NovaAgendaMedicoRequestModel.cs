using FluentValidation;
using MedicalHealth.Fiap.Dominio.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MedicalHealth.Fiap.Aplicacao.DTO
{
    public class NovaAgendaMedicoRequestModel
    {
        public DateTime Data { get; private set; }
        public TimeOnly HorarioInicio { get; private set; }
        public TimeOnly HorarioFim { get; private set; }
        public int Intervalo { get; private set; }
        public Guid MedicoId { get; private set; }
    }

    public class NovaAgendaMedicoRequestModelValidator : AbstractValidator<NovaAgendaMedicoRequestModel>
    {
        public NovaAgendaMedicoRequestModelValidator()
        {
            RuleFor(x => x.Data)
                .NotEmpty()
                .WithMessage(MensagemErroContato.MENSAGEM_NOME_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemErroContato.MENSAGEM_NOME_NAO_PODE_SER_NULO);

            RuleFor(x => x.HorarioInicio)
                .NotNull()
                .WithMessage(MensagemErroContato.MENSAGEM_DDD_NAO_PODE_SER_NULO)
                .Must(ddd => DDDValidator.DDDsValidos.Contains(ddd.ToString()))
                .WithMessage(MensagemErroContato.MENSAGEM_DDD_INVALIDO);

            RuleFor(x => x.HorarioFim)
                .NotEmpty()
                .WithMessage(MensagemErroContato.MENSAGEM_TELEFONE_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemErroContato.MENSAGEM_TELEFONE_NAO_PODE_SER_NULO)
                .MinimumLength(8)
                .WithMessage(MensagemErroContato.MENSAGEM_TELEFONE_MINIMO_OITO_CARACTERES);

            RuleFor(x => x.Intervalo)
                    .NotEmpty()
                    .WithMessage(MensagemErroContato.MENSAGEM_EMAIL_NAO_PODE_SER_VAZIO)
                    .NotNull()
                    .WithMessage(MensagemErroContato.MENSAGEM_EMAIL_NAO_PODE_SER_NULO)
                    .EmailAddress()
                    .WithMessage(MensagemErroContato.MENSAGEM_EMAIL_NAO_ESTA_NO_FORMATO_CORRETO);

            RuleFor(x => x.MedicoId)
                    .NotEmpty()
                    .WithMessage(MensagemErroContato.MENSAGEM_EMAIL_NAO_PODE_SER_VAZIO)
                    .NotNull()
                    .WithMessage(MensagemErroContato.MENSAGEM_EMAIL_NAO_PODE_SER_NULO)
                    .EmailAddress()
                    .WithMessage(MensagemErroContato.MENSAGEM_EMAIL_NAO_ESTA_NO_FORMATO_CORRETO);
        }
    }
}
