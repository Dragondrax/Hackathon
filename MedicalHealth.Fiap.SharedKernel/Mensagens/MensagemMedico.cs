namespace MedicalHealth.Fiap.SharedKernel.MensagensErro
{
    public static class MensagemMedico
    {
        public static string ErroNaoPodeAtualizarValorDaConsultaAposAceite = "Ops, você não pode atualizar o valor dessa consulta pois você já aceitou";
        public static string CRM_Nao_Pode_Ser_Vazio = "Ops, o CRM não pode ser vazio";
        public static string CRM_Nao_Pode_Ser_Nulo = "Ops, o CRM não pode ser nulo";
        public static string CRM_Nao_Esta_No_Formato_Correto = "Ops, o CRM não está no formato correto (ex: 123456-SP)";
        public static string CRM_Medico_Nao_Encontrado = "Ops, o(a) médico(a) não foi encontrado(a) com o CRM informado.";
        public static string Especialidade_Nenhum_Medico_Encontrado = "Ops, nenhum(a) médico(a) foi encontrado(a) com a especialidade informada";
    }
}
