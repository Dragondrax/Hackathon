namespace MedicalHealth.Fiap.SharedKernel.MensagensErro
{
    public static class MensagemMedico
    {
        public static string ERRONAOPODEATUALIZARVALORDACONSULTAAPOSACEITE = "Ops, você não pode atualizar o valor dessa consulta pois você já aceitou";
        public static string CRM_NAO_PODE_SER_VAZIO = "Ops, o CRM não pode ser vazio";
        public static string CRM_NAO_PODE_SER_NULO = "Ops, o CRM não pode ser nulo";
        public static string CRM_NAO_ESTA_NO_FORMATO_CORRETO = "Ops, o CRM não está no formato correto (ex: 123456-SP)";
        public static string CRM_MEDICO_NAO_ENCONTRADO = "Ops, o(a) médico(a) não foi encontrado(a) com o CRM informado.";
        public static string MENSAGEM_NENHUM_MEDICO_ENCONTRADO = "Ops, nenhum(a) médico(a) foi encontrado(a) com a especialidade informada";
        public static string MENSAGEM_ESPECIALIDADE_NAO_PODE_SER_NULO = "Ops, o role não pode ser nulo!";
        public static string MENSAGEM_ESPECIALIDADE_NAO_EXISTE = "Ops, não encontramos nenhum médico nessa especialidade!";
        public static string MENSAGEM_CRM_JA_EXISTENTE = "Ops, parece que esse CRM já existe na nossa base de dados!";
        public static string MENSAGEM_NOME_NAO_PODE_SER_VAZIO = "Ops, o nome não pode ser vazio!";
        public static string MENSAGEM_NOME_NAO_PODE_SER_NULO = "Ops, o nome não pode ser nulo!";
        public static string MENSAGEM_EMAIL_NAO_PODE_SER_VAZIO = "Ops, o email não pode ser vazio!";
        public static string MENSAGEM_EMAIL_NAO_PODE_SER_NULO = "Ops, o email não pode ser nulo!";
        public static string MENSAGEM_EMAIL_NAO_ESTA_NO_FORMATO_CORRETO = "Ops, o email não está no formato correto!";
        public static string MENSAGEM_VALOR_NAO_PODE_SER_VAZIO = "Ops, o valor não pode ser vazio!";
        public static string MENSAGEM_VALOR_NAO_PODE_SER_NULO = "Ops, o valor não pode ser nulo!";
        public static string MENSAGEM_VALOR_NAO_PODE_ZERO = "Ops, o valor deve ser maior que Zero!";
        public static string MENSAGEM_MEDICO_NAO_ENCONTRADO = "Ops, nenhum medico foi encontrado!";
    }
}
