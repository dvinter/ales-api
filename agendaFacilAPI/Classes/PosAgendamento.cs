namespace agendaFacilAPI.Classes
{
    public class PosAgendamento
    {
        public int idAgenda { get; set; }
        public int idTipoDocumento { get; set; }
        public string documentoNumero { get; set; }
        public string novaData { get; set; }
        public string novoHorario { get; set; }
        public int acao { get; set; }
    }
}