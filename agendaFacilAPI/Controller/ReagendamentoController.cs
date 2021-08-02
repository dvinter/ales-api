using System;
using agendaFacilAPI.Filters;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Collections.Generic;
using Intranet.SAGE.RegraNegocio;
using agendaFacilAPI.Database;
using System.Linq;
using agendaFacilAPI.Classes;

namespace agendaFacilAPI.Controller
{
    [BasicAuthentication]
    public class ReagendamentoController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public DatasDisponiveis Get(int id)
        {
            DatasDisponiveis datas = new DatasDisponiveis();

            datas.data = ListaDataReagendamento(id);

            return datas;
        }


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public DatasDisponiveis Get(int id, string data)
        {
            DatasDisponiveis datas = new DatasDisponiveis();
            datas.data = new List<DateTime>();
            datas.hora = new List<string>();

            List<DateTime> retorno = ListaHoraReagendamento(id, data);

            foreach (var item in retorno)
            {
                datas.data.Add(item.Date);
                datas.hora.Add(item.Hour +":"+ item.Minute);
            }

            return datas;
        }

        public List<DateTime> ListaDataReagendamento(int idServico)
        {
            List<DateTime> listaDataDisponivel = new List<DateTime>();

            try
            {
                listaDataDisponivel = new clsAgendaDAO(BuscarConexaoBD.BuscarConexao())
                    .ListarDatasDisponiveis(idServico).ToList();

                return listaDataDisponivel;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public List<DateTime> ListaHoraReagendamento(int idServico, string dataServico)
        {
            List<DateTime> listaHorarioDisponivel = new List<DateTime>();

            if ((!idServico.Equals(0)) && (!string.IsNullOrEmpty(dataServico)))
            {
                try
                {
                    DateTime data = Convert.ToDateTime(dataServico);

                    listaHorarioDisponivel = new clsAgendaDAO(BuscarConexaoBD.BuscarConexao())
                                                               .ListarHorariosDisponiveis(idServico, data).ToList();
                }
                catch (Exception e)
                {
                    return null;
                }
            }

            return listaHorarioDisponivel;
        }
    }
}
