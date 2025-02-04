namespace MedicalHealth.Fiap.Dominio.Entidades
{
    public class AgendaMedico : EntidadeBase
    {
        public DateTime Data { get; private set; }
        public TimeOnly HorarioInicio { get; private set; }
        public TimeOnly HorarioFim { get; private set; }
        public int Intervalo { get; private set; }
        public bool Disponivel { get; private set; }

        public AgendaMedico(DateTime data, TimeOnly horarioInicio, TimeOnly horarioFim, int intervalo, bool disponivel)
        {
            Data = data;
            HorarioInicio = horarioInicio;
            HorarioFim = horarioFim;
            Intervalo = intervalo;
            Disponivel = disponivel;
        }

        public void AtualizarHorarioIndisponivel()
        {
            Disponivel = false;
            AtualizarDataAtualizacao();
        }

        public void AtualizarHorarioDisponivel()
        {
            Disponivel = true;
            AtualizarDataAtualizacao();
        }

        public void AtualizarAgendaMedico(DateTime data, TimeOnly horarioInicio, TimeOnly horarioFim, int intervalo, bool disponivel)
        {
            Data = data;
            HorarioInicio = horarioInicio;
            HorarioFim = horarioFim;
            Intervalo = intervalo;
            Disponivel = disponivel;
            AtualizarDataAtualizacao();
        }

        public void Excluir()
        {
            Desativar();
        }
    }
}
