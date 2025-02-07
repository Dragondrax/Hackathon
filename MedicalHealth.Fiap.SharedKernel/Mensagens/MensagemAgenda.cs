namespace MedicalHealth.Fiap.SharedKernel.MensagensErro
{
    public static class MensagemAgenda
    {
        public static string MENSAGEM_HORARIO_COM_PACIENTE = "Ops, horário com pacientes agendados não podem ser alterados!";
        public static string MENSAGEM_HORARIO_INDISPONIVEL = "Ops, esse horário já está reservado, tente novamente mais tarde!";
        public static string MENSAGEM_DATA_NAO_PODE_SER_NULO_OU_VAZIO = "Ops, data não pode ser nulo ou vazio";
        public static string MENSAGEM_HORARIO_INICIO_NAO_PODE_SER_NULO_OU_VAZIO = "Ops, horario inicio não pode ser nulo ou vazio";
        public static string MENSAGEM_HORARIO_FIM_NAO_PODE_SER_NULO_OU_VAZIO = "Ops, horario fim não pode ser nulo ou vazio";
        public static string MENSAGEM_ID_NAO_PODE_SER_NULO_OU_VAZIO = "Ops, id não pode ser nulo ou vazio";
        public static string MENSAGEM_INTERVALO_NAO_PODE_SER_NULO = "Ops, intervalo não pode ser nulo";
        public static string MENSAGEM_TEMPO_CONSULTA_NAO_PODE_SER_NULO = "Ops, tempo da consulta não pode ser nulo";
        public static string MENSAGEM_MEDICO_ID_NAO_PODE_SER_NULO_OU_VAZIO = "Ops, MedicoId não pode ser nulo ou vazio";
        public static string MENSAGEM_PERIDO_DE_CRIACAO_NAO_PODE_SER_MAIOR_QUE_30_DIAS_E_ANTES_DE_HOJE = "Ops, você não pode criar uma agenda anterior a hoje e com mais de 30 dias";
        public static string MENSAGEM_AGENDA_JA_EXISTENTE = "Ops, parece que o range selecionado já existe na sua agenda, por favor escolha periodos ainda não criados.";
        public static string MENSAGEM_AGENDA_NAO_EXISTENTE = "Ops, não conseguimos encontrar uma agenda ativa para você!";
        public static string MENSAGEM_DIA_HORARIO_JA_OCUPADOS_NAO_PODEM_SER_EXCLUIDOS = "Ops, parece que o dia e horario selecionado já possui paciente e não pode ser excluido.";
        public static string MENSAGEM_SUCESSO_PARCIAL_EXCLUSAO_HORARIOS = "Os dias que não possuem pacientes agendados foram excluidos com sucesso!";
        public static string MENSAGEM_MEDICO_SEM_AGENDA_DISPONIVEL = "Ops, o médico não possue agenda disponível";
        public static string MENSAGEM_AGENDA_SEM_VALOR_DE_CONSULTA_DEFINIDO = "Ops, o médico não possui valor para consulta, entrar em contato pelo email medicaHealt@medical.com.br";
    }
}
