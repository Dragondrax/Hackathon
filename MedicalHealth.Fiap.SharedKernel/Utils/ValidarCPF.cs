using System.Text.RegularExpressions;

namespace MedicalHealth.Fiap.SharedKernel.Utils
{
    public static class ValidarCPF
    {
        public static bool CpfValido(string cpf)
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
