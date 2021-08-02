using Intranet.SAGE.RegraNegocio;

namespace agendaFacilAPI.Classes
{
    public class Agendamento
    {

        public int id { get; set; }
        public string data { get; set; }
        public string hora { get; set; }
        public clsAgendaAtendimento dados { get; set; }
        public string numeroDocumento { get; set; }
    }
}