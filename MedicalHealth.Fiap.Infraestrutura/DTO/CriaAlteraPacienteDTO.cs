﻿using FluentValidation;
using MedicalHealth.Fiap.SharedKernel.MensagensErro;
using System.Text.RegularExpressions;

namespace MedicalHealth.Fiap.Infraestrutura.DTO
{
    public class CriarAlterarPacienteDTO
    {
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
    }

    public class CriarAlterarPacienteDTOValidator : AbstractValidator<CriarAlterarPacienteDTO>
    {
        public CriarAlterarPacienteDTOValidator()
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
                .WithMessage(MensagemPaciente.MENSAGEM_CPF_NAO_PODE_SER_NULO)
                .Must(ValidarCPF)
                .WithMessage(MensagemPaciente.MENSAGEM_CPF_INVALIDO);

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(MensagemPaciente.MENSAGEM_EMAIL_NAO_PODE_SER_VAZIO)
                .NotNull()
                .WithMessage(MensagemPaciente.MENSAGEM_EMAIL_NAO_PODE_SER_NULO)
                .EmailAddress()
                .WithMessage(MensagemPaciente.MENSAGEM_EMAIL_NAO_ESTA_NO_FORMATO_CORRETO);
        }

        private bool ValidarCPF(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
                return false;

            cpf = Regex.Replace(cpf, @"[^\d]", "");

            if (cpf.Length != 11)
                return false;

            if (new Regex(@"^(\d)\1*$").IsMatch(cpf))
                return false;

            int[] multiplicadores1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicadores2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            var soma1 = 0;
            for (int i = 0; i < 9; i++)
                soma1 += (cpf[i] - '0') * multiplicadores1[i];

            var digito1 = soma1 % 11 < 2 ? 0 : 11 - soma1 % 11;
            if (digito1 != cpf[9] - '0') return false;

            var soma2 = 0;
            for (int i = 0; i < 10; i++)
                soma2 += (cpf[i] - '0') * multiplicadores2[i];

            var digito2 = soma2 % 11 < 2 ? 0 : 11 - soma2 % 11;
            return digito2 == cpf[10] - '0';
        }
    }
}
