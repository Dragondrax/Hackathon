namespace MedicalHealth.Fiap.Dominio.Entidades;

public class AgendaMedica : EntidadeBase
{
    public Guid MedicoId { get; private set; }
    public Medico? Medico { get; private set; }
    public DateTime Data { get; private set; }
    public TimeSpan HorarioInicio { get; private set; }
    public TimeSpan HorarioFim { get; private set; }
    public TimeSpan Intervalo { get; private set; }
    public TimeSpan? HorarioAlmoco { get; private set; }

    public AgendaMedica() { }

    public AgendaMedica(Guid medicoId, DateTime data, TimeSpan horarioInicio, TimeSpan horarioFim, TimeSpan intervalo, TimeSpan? horarioAlmoco)
    {
        MedicoId = medicoId;
        Data = data.Date;
        HorarioInicio = horarioInicio;
        HorarioFim = horarioFim;
        Intervalo = intervalo;
        HorarioAlmoco = horarioAlmoco;
    }

    public void AlterarHorario(TimeSpan novoInicio, TimeSpan novoFim, TimeSpan novoIntervalo, TimeSpan? novoAlmoco)
    {
        HorarioInicio = novoInicio;
        HorarioFim = novoFim;
        Intervalo = novoIntervalo;
        HorarioAlmoco = novoAlmoco;
        AtualizarDataAtualizacao();
    }

    public void ExcluiAgendaMedica()
    {
        Excluir();
    }
}
