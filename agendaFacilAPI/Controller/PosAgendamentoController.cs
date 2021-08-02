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
    public class PosAgendamentoController : ApiController
    {
        public object PostPosAgendamento(PosAgendamento posAgendamento)
        {
            Retorno retorno = new Retorno();

            int idAgenda = posAgendamento.idAgenda;
            int idTipoDocumento = posAgendamento.idTipoDocumento;
            string documentoNumero = posAgendamento.documentoNumero;
            string novaData = posAgendamento.novaData;
            string novoHorario = posAgendamento.novoHorario;
            int acao = posAgendamento.acao;
            /*
            * Ações possíveis a partir da consulta:
            * 
            * 0 - sem ação selecionada
            * 1 - consulta
            * 2 - exclusão (cancelamento)
            * 3 - reagendamento
            * 
            */
            string msgRetorno = string.Empty;
            List<clsAgendaAtendimento> listaAgendamento = null;
            clsAgendaAtendimento agendaAtendimento = null;

            switch (acao)
            {
                case 1: //Consulta

                    try
                    {
                        listaAgendamento = new clsAgendaAtendimentoDAO(BuscarConexaoBD.BuscarConexao()).ListarViaDoc(idTipoDocumento, documentoNumero);
                    }
                    catch (Exception ex) { }


                    //Verifica se existem agendamentos para tipo/documento informado
                    if (!listaAgendamento.Any())
                    {
                        //Caso não existam registros
                        retorno.mensagem = "Não foram encontrados agendamentos para este serviço/documento.";
                        return retorno;
                    }
                    else
                    {

                        if (listaAgendamento[0].ConfirmouEmail)
                        {
                            //return "E-mail/Agendamento confirmado.";
                            return listaAgendamento[0];
                        }
                        else
                        {
                            retorno.mensagem = "Aguardando confirmação do E-mail.";
                            return retorno;
                        }
                    }

                    break;

                case 2: //Cancela

                    //Busca os dados do agendamento (informa o ID do agendamento)
                    agendaAtendimento = new clsAgendaAtendimentoDAO(BuscarConexaoBD.BuscarConexao()).Buscar(idAgenda);

                    try
                    {
                        msgRetorno = string.Empty;
                        new clsAgendaAtendimentoDAO(BuscarConexaoBD.BuscarConexao()).Cancelar(agendaAtendimento);
                        msgRetorno = "Atendimento cancelado. A data " + agendaAtendimento.Agenda.DataAgendamento.ToString("dd/MM/yyyy HH:mm") + " foi liberada para nova agenda!";

                        retorno.mensagem = msgRetorno;
                        return retorno;
                    }
                    catch (RegraNegocioException ex)
                    {
                        retorno.mensagem = ex.Message;
                        return retorno;
                    }
                    catch (Exception ex)
                    {
                        retorno.mensagem = ex.Message;
                        return retorno;
                    }

                    break;

                case 3: //Reagenda

                    //Busca os dados do agendamento (informa o ID do agendamento)
                    agendaAtendimento = new clsAgendaAtendimentoDAO(BuscarConexaoBD.BuscarConexao()).Buscar(idAgenda);

                    DateTime novaDataReagenda = DateTime.Now;

                    try
                    {
                        //Valida e ajusta o formato da Data e Hora do agendamento
                        novaDataReagenda = Convert.ToDateTime(novaData + " " + novoHorario);
                    }
                    catch
                    {
                        retorno.mensagem = "A data informada é Inválida!!!";
                        return retorno;
                    }

                    try
                    {
                        msgRetorno = String.Empty;
                        agendaAtendimento.Reagendar(BuscarConexaoBD.BuscarConexao(), novaDataReagenda);
                        retorno.mensagem = "Reagendamento realizado com sucesso!";
                        return retorno;

                    }
                    catch (Exception ex)
                    {
                        retorno.mensagem = ex.Message;
                        return retorno;
                    }


                    break;

                default:
                    break;
            }

            retorno.mensagem = msgRetorno;
            return retorno;
        }
    }
}