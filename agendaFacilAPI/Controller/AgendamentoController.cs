using System;
using agendaFacilAPI.Filters;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Collections.Generic;
using Intranet.SAGE.RegraNegocio;
using agendaFacilAPI.Database;
using System.Linq;
using agendaFacilAPI.Classes;
using Componente;
using Newtonsoft.Json;
using System.Web.Http.Results;

namespace agendaFacilAPI.Controller
{

    [BasicAuthentication]
    [RoutePrefix("api/Agendamento")]
    public class AgendamentoController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public object Get(int id)
        {
            DatasDisponiveis datas = new DatasDisponiveis();

            return ListarDatasDisponiveis(id);
        }


        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public object Get(int id, string data)
        {
            DatasDisponiveis datas = new DatasDisponiveis();
            datas.data = new List<DateTime>();
            datas.hora = new List<string>();

            List<DateTime> retorno = ListarHorariosDisponiveis(id, data);

            foreach (var item in retorno)
            {
                datas.data.Add(Convert.ToDateTime(item.ToShortDateString()));
                datas.hora.Add(item.ToShortTimeString());
            }

            return datas;
        }


        public object ListarDatasDisponiveis(int idServico)
        {
            List<DateTime> listaDataDisponivel = new List<DateTime>();
            Retorno retorno = new Retorno();

            try
            {
                listaDataDisponivel = new clsAgendaDAO(BuscarConexaoBD.BuscarConexao())
                                                .ListarDatasDisponiveis(idServico).ToList();

            }
            catch (Exception e)
            {
                retorno.mensagem = e.Message;
                return retorno;
            }

            return listaDataDisponivel;
        }

        public List<DateTime> ListarHorariosDisponiveis(int idServico, string dataServico)
        {
            List<DateTime> listaHorarioDisponivel = new List<DateTime>();
            Retorno retorno = new Retorno();

            if ((!idServico.Equals(0)) && (!string.IsNullOrEmpty(dataServico)))
            {
                DateTime data = Convert.ToDateTime(dataServico);

                listaHorarioDisponivel = new clsAgendaDAO(BuscarConexaoBD.BuscarConexao())
                                                                    .ListarHorariosDisponiveis(idServico, data).ToList();
            }

            return listaHorarioDisponivel;
        }

        public object PostFinalizaAgendamento(Agendamento agendamento)
        {
            Retorno retorno = new Retorno();
            string data = string.Empty;

            int id = agendamento.id;

            if (agendamento.data != null)
                data = agendamento.data;
            


            string hora = agendamento.hora;
            clsAgendaAtendimento dados = agendamento.dados;
            string numeroDocumento = agendamento.numeroDocumento;

            try
            {
                //Valida e ajusta o formato da Data e Hora do agendamento
                dados.Agenda.TipoAgenda.Id = id;
                dados.Agenda.DataAgendamento = Convert.ToDateTime(data + " " + hora);

                //Identifica o tipo e número do documento informado
                dados.ListaDocumentos.Add(new clsDocumento()
                {
                    TipoDocumento = new clsTipoDocumento()
                    {
                        Id = 1
                    },
                    Valor = numeroDocumento

                });

                //Valida os dados e insere o registro na base de dados, retornando as mensagens cabíveis
                string msgRetorno = string.Empty;
                msgRetorno = dados.Inserir(BuscarConexaoBD.BuscarConexao(),
                                    System.Configuration.ConfigurationManager.AppSettings["strMailHost"],
                                    System.Configuration.ConfigurationManager.AppSettings["strMailPort"],
                                    System.Configuration.ConfigurationManager.AppSettings["strMailSender"]);


            }
            catch (Exception ex)
            {
                retorno.mensagem = ex.Message;
                return retorno;
            }

            retorno.mensagem = "Agendamento realizado com sucesso.";
            return retorno;

        }
    }
}