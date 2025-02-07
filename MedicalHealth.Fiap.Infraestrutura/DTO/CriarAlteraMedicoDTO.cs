﻿using FluentValidation;
using MedicalHealth.Fiap.Infraestrutura.Enum;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using System.Text.RegularExpressions;

namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class CriarAlteraMedicoDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string CRM { get; set; }
        public string Email { get; set; }
        public double ValorConsulta { get;  set; }
        public Especialidade EspecialidadeMedica { get; set; }
    }

    public class CriarAlteraMedicoDTOValidator : AbstractValidator<CriarAlteraMedicoDTO>
    {
        public CriarAlteraMedicoDTOValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty()
                .WithMessage(MensagemMedico.MENSAGEM_NOME_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemMedico.MENSAGEM_NOME_NAO_PODE_SER_NULO);

            RuleFor(x => x.CPF)
                .NotEmpty()
                .WithMessage(MensagemPaciente.MENSAGEM_CPF_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemPaciente.MENSAGEM_CPF_NAO_PODE_SER_NULO);
                //.Must(ValidarCPF)
                //.WithMessage(MensagemPaciente.MENSAGEM_CPF_INVALIDO);

            RuleFor(x => x.CRM)
                .NotEmpty()
                .WithMessage(MensagemMedico.CRM_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemMedico.CRM_NAO_PODE_SER_NULO)
                .Matches(@"^\d{4,6}-[A-Z]{2}$")
                .WithMessage(MensagemMedico.CRM_NAO_ESTA_NO_FORMATO_CORRETO);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(MensagemMedico.MENSAGEM_EMAIL_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemMedico.MENSAGEM_EMAIL_NAO_PODE_SER_NULO)
                .EmailAddress()
                .WithMessage(MensagemMedico.MENSAGEM_EMAIL_NAO_ESTA_NO_FORMATO_CORRETO);

            RuleFor(x => x.ValorConsulta)
                .NotEmpty()
                .WithMessage(MensagemMedico.MENSAGEM_VALOR_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemMedico.MENSAGEM_VALOR_NAO_PODE_SER_NULO)
                .GreaterThan(0).WithMessage(MensagemMedico.MENSAGEM_VALOR_NAO_PODE_ZERO);


            RuleFor(x => x.EspecialidadeMedica)
                .NotNull()
                .WithMessage(MensagemMedico.MENSAGEM_ESPECIALIDADE_NAO_PODE_SER_NULO)
                .IsInEnum()
                .WithMessage(MensagemMedico.MENSAGEM_ESPECIALIDADE_NAO_EXISTE);

        }

        private bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf)) return false;

            cpf = cpf.Trim().Replace(".", "").Replace("-", ""); // Remove pontos e traço

            // Verifica se o CPF tem 11 dígitos
            if (cpf.Length != 11 || !Regex.IsMatch(cpf, @"^\d{11}$"))
            {
                return false;
            }

            // Verifica se o CPF é composto por todos os mesmos números (ex: 111.111.111-11)
            if (cpf.All(c => c == cpf[0]))
            {
                return false;
            }

            // Calcula os dois dígitos verificadores
            int[] multiplicadores1 = new int[] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = new int[] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            int soma1 = 0, soma2 = 0;

            for (int i = 0; i < 9; i++)
            {
                soma1 += (cpf[i] - '0') * multiplicadores1[i];
                soma2 += (cpf[i] - '0') * multiplicadores2[i];
            }

            int digito1 = (soma1 % 11 < 2) ? 0 : 11 - (soma1 % 11);
            int digito2 = (soma2 % 11 < 2) ? 0 : 11 - (soma2 % 11);

            return cpf[9] == digito1.ToString()[0] && cpf[10] == digito2.ToString()[0];
        }
    }
}
