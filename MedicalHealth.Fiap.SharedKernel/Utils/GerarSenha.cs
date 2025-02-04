namespace MedicalHealth.Fiap.SharedKernel.Utils
{
    public static class GerarSenha
    {
        public static string Aleatoria()
        {
            Random random = new Random();

            var caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ!@#$%^&*()1234567890";
            var senha = "";

            for (var i = 0; i <= 10; i++)
            {
                var numeroAleatorio = random.Next(0, 71);
                senha += caracteres[numeroAleatorio];
            }

            return senha;
        }
    }
}
