//// Decompiled with JetBrains decompiler
//// Type: Intranet.SAGE.RegraNegocio.clsAgendaAtendimento
//// Assembly: Intranet.SAGE.RegraNegocio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: E983EC8F-1008-4D0E-AFF6-43BA4207D9C3
//// Assembly location: C:\Users\davi.vinter.INTELLI019\Downloads\Intranet.SAGE.RegraNegocio.dll

//using Componente;
//using Intranet.SAGE.RegraNegocio;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Net;
//using System.Net.Mail;

//namespace agendaFacilAPI.DAO
//{
//    public class clsAgendaAtendimentoDecompilado
//    {
//        public string Conexao { get; set; }

//        public clsAgendaAtendimentoDecompilado(string _conexao)
//        {
//            this.Conexao = _conexao;
//        }

//        public clsAgenda Agenda { get; set; }

//        public clsAgenda AgendaAnterior { get; set; }

//        public int Id { get; set; }

//        public string NomeCidadao { get; set; }

//        public string Telefone { get; set; }

//        public string Celular { get; set; }

//        public string Email { get; set; }

//        public bool EstahAtivo { get; set; }

//        public int MatriculaCadastro { get; set; }

//        public int MatriculaCancelamento { get; set; }

//        public DateTime DataCadastro { get; set; }

//        public DateTime? DataConfirmacaoEmail { get; set; }

//        public DateTime? DataConfirmacaoLembrete { get; set; }

//        public bool ConfirmouEmail
//        {
//            get
//            {
//                return this.DataConfirmacaoEmail.HasValue;
//            }
//        }

//        public bool ConfirmouLembrete
//        {
//            get
//            {
//                return this.DataConfirmacaoLembrete.HasValue;
//            }
//        }

//        public List<clsDocumento> ListaDocumentos { get; set; }

//        public clsAgendaAtendimentoDecompilado()
//        {
//            this.Agenda = new clsAgenda();
//            this.ListaDocumentos = new List<clsDocumento>();
//        }

//        public clsDocumento BuscaDocumento(int _IdTipoDocumento)
//        {
//            clsDocumento clsDocumento = (clsDocumento)null;
//            foreach (clsDocumento listaDocumento in this.ListaDocumentos)
//            {
//                if (listaDocumento.TipoDocumento.Id == _IdTipoDocumento)
//                {
//                    clsDocumento = listaDocumento;
//                    break;
//                }
//            }
//            return clsDocumento;
//        }

//        public DateTime BuscaPrazoConfirmacaoEmail()
//        {
//            return this.DataCadastro.AddMinutes((double)this.Agenda.TipoAgenda.QtdMinConfirmacaoEmail);
//        }

//        public bool VerificaToleranciaAtendimento()
//        {
//            return DateTime.Compare(this.Agenda.DataAgendamento, DateTime.Now.AddMinutes((double)(this.Agenda.TipoAgenda.QtdMinTolerancia * -1))) >= 0;
//        }

//        public string Inserir(string _conexao, string host, string porta, string remetente)
//        {
//            bool disparoEmail = false;
//            clsAgenda clsAgenda = new clsAgendaDAO(_conexao).BuscarDisponivel(this.Agenda.TipoAgenda.Id, this.Agenda.DataAgendamento);
//            if (clsAgenda == null)
//                throw new RegraNegocioException("O Horário escolhido não está mais disponível para agendamento. Selecione outro horário.");
//            this.Agenda = clsAgenda;
//            //this.Validar(_conexao);
//            if (this.Agenda.TipoAgenda.Id == 1)
//            {
//                DateTime dateTime = new clsAgendaAtendimentoDAO(_conexao).BuscaDataMaxPenalidade(1, 1, this.BuscaDocumento(1).Valor);
//                if (dateTime > this.Agenda.DataAgendamento)
//                    throw new RegraNegocioException("Segundo as regras para o CPF informado não poderá reservar horários até " + dateTime.ToShortDateString());
//                    DateTime? nullable = new clsAgendaAtendimentoDAO(_conexao).VerificarDocOutroAtendimento(1, 1, this.BuscaDocumento(1).Valor);
//                if (nullable.HasValue)
//                    throw new RegraNegocioException("Existe atendimento reservado para " + nullable.Value.ToString("dd/MM/yyyy HH:mm"));
//            }
//            _Inserir(this, host, porta, remetente, ref disparoEmail);
//            string empty = string.Empty;
//            string str ="";
//            if (disparoEmail)
//                str = "Foi encaminhado e-mail para '" + this.Email + "' com instruções para confirmação do agendamento.<br/> Caso não haja confirmação no prazo de <span style=\"color:red;font-weight:bold\">";
//            //+ Util.HoraAbreviada(this.Agenda.TipoAgenda.QtdMinConfirmacaoEmail) + "</span> o agendamento será cancelado!";
//            else
//                str = "Agendamento realizado com sucesso para a data de HOJE (<span style=\"color:red;font-weight:bold\">" + this.Agenda.DataAgendamento.ToString("HH:mm") + "</span>)!";
//            return str;
//        }



//        public void _Inserir(
//  clsAgendaAtendimentoDecompilado _AgendaAtendimento,
//  string host,
//  string porta,
//  string remetente,
//  ref bool disparoEmail)
//        {
//            SqlTransaction sqlTransaction = (SqlTransaction)null;
//            using (SqlConnection sqlConnection = new SqlConnection(agendaFacilAPI.Database.BuscarConexaoBD.BuscarConexao()))
//            {
//                string cmdText1 = " INSERT INTO SAGE_AGENDA_ATENDIMENTO(ID_AGENDA, NM_CIDADAO, DT_CADASTRO, NR_MAT_CADASTRO, NR_TELEFONE, NR_CELULAR, TX_EMAIL, TX_CHAVE_CONFIRMACAO, ID_TIPO_SITUACAO, DT_CONFIR_EMAIL, DT_CONFIR_LEMBRETE)  VALUES (@ID_AGENDA, @NM_CIDADAO, GETDATE(), @NR_MAT_CADASTRO, @NR_TELEFONE, @NR_CELULAR, @TX_EMAIL, '',  @TIPO_SITUACAO, @DT_CONFIR_EMAIL, (case when cast(floor(cast(GETDATE() as float)) as datetime) >= DATEADD(DAY, 4 * (-1), cast(floor(cast(@DATA_AGENDA as float)) as datetime) ) then GETDATE() else NULL END ));SELECT SCOPE_IDENTITY();";
//                string cmdText2 = " UPDATE SAGE_AGENDA_ATENDIMENTO " + Environment.NewLine + "      SET TX_CHAVE_CONFIRMACAO = @CHAVE " + Environment.NewLine + " WHERE ID_AGENDA_ATENDIMENTO = @ID  " + Environment.NewLine + " AND DT_CANCELAMENTO IS NULL ";
//                try
//                {
//                    sqlConnection.Open();
//                    sqlTransaction = sqlConnection.BeginTransaction();
//                    if (!new clsAgendaDAO(this.Conexao).VerificarHorarioDisponivel(_AgendaAtendimento.Agenda.TipoAgenda.Id, _AgendaAtendimento.Agenda.DataAgendamento, sqlConnection, sqlTransaction))
//                        throw new RegraNegocioException("Essa Data está Indisponível! Outra pessoa a reservou.");
//                    bool flag = _AgendaAtendimento.Agenda.DataAgendamento.ToString("dd/MM/yyyy").Equals(DateTime.Now.ToString("dd/MM/yyyy"));
//                    disparoEmail = !flag;
//                    SqlCommand sqlCommand1 = new SqlCommand(cmdText1, sqlConnection, sqlTransaction);
//                    sqlCommand1.Parameters.AddWithValue("@ID_AGENDA", (object)_AgendaAtendimento.Agenda.Id);
//                    sqlCommand1.Parameters.AddWithValue("@NM_CIDADAO", (object)_AgendaAtendimento.NomeCidadao);
//                    if (_AgendaAtendimento.MatriculaCadastro > 0)
//                    {
//                        sqlCommand1.Parameters.AddWithValue("@NR_MAT_CADASTRO", (object)_AgendaAtendimento.MatriculaCadastro);
//                        sqlCommand1.Parameters.AddWithValue("@TIPO_SITUACAO", (object)2);
//                    }
//                    else
//                    {
//                        sqlCommand1.Parameters.AddWithValue("@NR_MAT_CADASTRO", (object)DBNull.Value);
//                        sqlCommand1.Parameters.AddWithValue("@TIPO_SITUACAO", (object)1);
//                    }
//                    sqlCommand1.Parameters.AddWithValue("@NR_TELEFONE", (object)_AgendaAtendimento.Telefone);
//                    sqlCommand1.Parameters.AddWithValue("@NR_CELULAR", (object)_AgendaAtendimento.Celular);
//                    sqlCommand1.Parameters.AddWithValue("@TX_EMAIL", (object)_AgendaAtendimento.Email);
//                    sqlCommand1.Parameters.AddWithValue("@DATA_AGENDA", (object)_AgendaAtendimento.Agenda.DataAgendamento);
//                    if (flag)
//                        sqlCommand1.Parameters.AddWithValue("@DT_CONFIR_EMAIL", (object)DateTime.Now);
//                    else
//                        sqlCommand1.Parameters.AddWithValue("@DT_CONFIR_EMAIL", (object)DBNull.Value);
//                    int int32 = Convert.ToInt32(sqlCommand1.ExecuteScalar());
//                    _AgendaAtendimento.Id = int32;
//                    string str1 = string.Empty;
//                    if (!flag)
//                    {
//                        str1 = Util.GerarChaveConfirmacao(int32.ToString());
//                        SqlCommand sqlCommand2 = new SqlCommand(cmdText2, sqlConnection, sqlTransaction);
//                        sqlCommand2.Parameters.AddWithValue("@ID", (object)_AgendaAtendimento.Id);
//                        sqlCommand2.Parameters.AddWithValue("@CHAVE", (object)str1);
//                        sqlCommand2.ExecuteNonQuery();
//                    }
//                    foreach (clsDocumento listaDocumento in _AgendaAtendimento.ListaDocumentos)
//                    {
//                        listaDocumento.AgendaAtendimento = _AgendaAtendimento;
//                        new clsDocumentoDAO(this.Conexao).Inserir(listaDocumento, sqlConnection, sqlTransaction);
//                    }
//                    if (!flag)
//                    {
//                        //string str2 = "https://www1.al.es.gov.br/Agendamento/ConfirmacaoEmail?chave=" + str1;
//                        string str2 = "http://10.243.151.21/Agendamento/ConfirmacaoEmail?chave=" + str1;

//                        SmtpClient smtpClient = new SmtpClient();
//                        smtpClient.Host = host;
//                        smtpClient.Port = int.Parse(porta);
//                        smtpClient.EnableSsl = true;
//                        smtpClient.UseDefaultCredentials = false;

//                        string userEmail = "assistente@intelliway.com.br";
//                        string passwordEmail = "Intelliway2017@";
//                        smtpClient.Credentials = new NetworkCredential(userEmail, passwordEmail);

//                        MailMessage message = new MailMessage();
//                        message.From = new MailAddress(remetente, "ALES");
//                        message.To.Add(new MailAddress(_AgendaAtendimento.Email, _AgendaAtendimento.NomeCidadao));
//                        message.Subject = "Sistema de Agendamento - ALES - Confirmação de E-mail...";
//                        message.Body = "<html>  " + Environment.NewLine + "<head> " + Environment.NewLine + "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" /> " + Environment.NewLine + "<title>Untitled Document</title> " + Environment.NewLine + "</head> " + Environment.NewLine + " " + Environment.NewLine + "<body> " + Environment.NewLine + "<table width=\"872\"  style=\"width: 900px; margin: 0px auto 0px auto;\"> " + Environment.NewLine + "<tr> " + Environment.NewLine + "<td colspan=\"3\" style=\" padding:0; \"><img src=\"http://www.al.es.gov.br/appdata/anexos_internet/DiariosPDF/imagens/perspectiva.jpg\" width=\"171\" height=\"155\" /></td> " + Environment.NewLine + "</tr> " + Environment.NewLine + "<tr> " + Environment.NewLine + "<td width=\"1\" background=\"http://www.al.es.gov.br/appdata/anexos_internet/DiariosPDF/imagens/lateral_email5.png\"> </td> " + Environment.NewLine + "<td style=\" padding:10px; \" width=\"758\" > " + Environment.NewLine + "Caro(a) " + _AgendaAtendimento.NomeCidadao + ",<br /> " + Environment.NewLine + "<br /> " + Environment.NewLine + "Clique no link abaixo para confirmar o seu cadastro. Esta validação deve ser feita em até " + DateTime.Now.AddMinutes((double)_AgendaAtendimento.Agenda.TipoAgenda.QtdMinConfirmacaoEmail).ToString("dd/MM/yyyy HH:mm") + ".<br /> " + Environment.NewLine + "<br /> " + Environment.NewLine + "<a href=\"" + str2 + "\" target=\"_blank\">Link de Confirmação do Usuario - Clique aqui.</a><br /> " + Environment.NewLine + "<br /> " + Environment.NewLine + "<strong>Por que uma mensagem de correio eletrônico de confirmação?</strong><br /> " + Environment.NewLine + "<ul> " + Environment.NewLine + "<li>Protege a sua identidade.</li> " + Environment.NewLine + "<li>Valida seu e-mail.</li> " + Environment.NewLine + "</ul> " + Environment.NewLine + "<p><br /> " + Environment.NewLine + "<strong>O Link de Confirmação da Conta não funciona?</strong><br /> " + Environment.NewLine + "Se não conseguir acessar o link acima, poderá terminar a confirmação do endereço de correio eletrônico copiando e colando o seguinte URL no seu navegador:<br /> " + Environment.NewLine + "<br /> " + Environment.NewLine + " <a href=\"" + str2 + "\" target=\"_blank\">" + str2 + "</a> <br /> " + Environment.NewLine + "<br /> " + Environment.NewLine + "<p><strong>Atenção</strong><br/>" + Environment.NewLine + _AgendaAtendimento.Agenda.TipoAgenda.MsgRegras + Environment.NewLine + "</p>" + Environment.NewLine + "<p>&nbsp;</p> " + Environment.NewLine + "<p>&nbsp;</p> " + Environment.NewLine + "<p>Esta é uma mensagem automática, favor não responder.</p> " + Environment.NewLine + "<p><img src=\"http://www.al.es.gov.br/appdata/anexos_internet/DiariosPDF/imagens/fim_linha.png\" /></p>  " + Environment.NewLine + "</td> " + Environment.NewLine + "<td width=\"48\">&nbsp;</td> " + Environment.NewLine + "</tr> " + Environment.NewLine + "<tr> " + Environment.NewLine + "<td colspan=\"3\" align=\"left\"><p>ASSEMBLEIA LEGISLATIVA DO ESTADO DO ESPÍRITO SANTO " + Environment.NewLine + "<br> " + Environment.NewLine + "Av. Américo Buaiz, 205 - Enseada do Suá - CEP: 29050-950 - Vitória - ES - BRASIL </p></td> " + Environment.NewLine + "</tr> " + Environment.NewLine + "</table> " + Environment.NewLine + "</body> " + Environment.NewLine + "</html> ";
//                        message.IsBodyHtml = true;
//                        message.Priority = MailPriority.High;
//                        smtpClient.Send(message);
//                    }
//                    sqlTransaction.Commit();
//                }
//                catch (Exception ex)
//                {
//                    sqlTransaction?.Rollback();
//                    throw ex;
//                }
//                finally
//                {
//                    sqlConnection?.Close();
//                }
//            }
//        }

//        //public void Atualizar(string _conexao)
//        //{
//        //  clsAgenda clsAgenda = new clsAgendaDAO(_conexao).Buscar(this.Agenda.TipoAgenda.Id, this.Agenda.DataAgendamento);
//        //  if (clsAgenda == null)
//        //    throw new RegraNegocioException("Data inválida. Essa Data está Indisponível (Possívelmente foi removida)!");
//        //  this.Agenda = clsAgenda;
//        //  this.Validar(_conexao);
//        //  new clsAgendaAtendimentoDAO(_conexao)._Atualizar(this);
//        //}

//        //public void Reagendar(string _conexao, DateTime novaData)
//        //{
//        //  clsAgenda clsAgenda = new clsAgendaDAO(_conexao).BuscarDisponivel(this.Agenda.TipoAgenda.Id, novaData);
//        //  if (clsAgenda == null)
//        //    throw new RegraNegocioException("Data inválida. Essa Data está Indisponível (Possívelmente foi reservada por outra pessoa antes de você salvar os dados)!");
//        //  this.Agenda = clsAgenda;
//        //  if (this.Agenda.TipoAgenda.Id == 1)
//        //  {
//        //    DateTime dateTime = new clsAgendaAtendimentoDAO(_conexao).BuscaDataMaxPenalidade(1, 1, this.BuscaDocumento(1).Valor);
//        //    if (dateTime > novaData)
//        //      throw new RegraNegocioException("Segundo as regras para o CPF informado não poderá reservar horários até " + dateTime.ToShortDateString());
//        //    DateTime? nullable = new clsAgendaAtendimentoDAO(_conexao).VerificarDocOutroAtendimento(1, 1, this.BuscaDocumento(1).Valor, this.Id);
//        //    if (nullable.HasValue)
//        //      throw new RegraNegocioException("Já existe um atendimento reservado para " + nullable.Value.ToString("dd/MM/yyyy HH:mm"));
//        //  }
//        //  new clsAgendaAtendimentoDAO(_conexao)._Reagendar(this);
//        //}

//        //private void Validar(string _conexao)
//        //{
//        //  int _idTipoAgenda = 0;
//        //  Util.AplicarVazioTodosCamposString((object) this);
//        //  Util.AplicarTrimTodosCampos((object) this);
//        //  string str = string.Empty;
//        //  if (this.Agenda == null)
//        //    str += "<br>- Data de Agendamento";
//        //  else if (this.Agenda.TipoAgenda == null)
//        //    str += "<br>- Tipo de Agenda";
//        //  else if (this.Agenda.TipoAgenda.Id <= 0)
//        //    str += "<br>- Tipo de Agenda (Cód)";
//        //  else
//        //    _idTipoAgenda = this.Agenda.TipoAgenda.Id;
//        //  if (string.IsNullOrEmpty(this.NomeCidadao.Trim()))
//        //    str += "<br>- Nome";
//        //  if (string.IsNullOrEmpty(this.Email.Trim()))
//        //    str += "<br>- Email";
//        //  else if (!Util.VerificarEmail(this.Email.Trim()))
//        //    str += "<br>- Email Inválido";
//        //  if (string.IsNullOrEmpty(this.Celular.Trim()))
//        //    str += "<br>- Celular";
//        //  foreach (clsDocumento listaDocumento1 in this.ListaDocumentos)
//        //  {
//        //    int num = 0;
//        //    foreach (clsDocumento listaDocumento2 in this.ListaDocumentos)
//        //    {
//        //      if (listaDocumento1.TipoDocumento.Id == listaDocumento2.TipoDocumento.Id)
//        //        ++num;
//        //    }
//        //    if (num > 1)
//        //      str = str + "<br>- Tipo Documento Cod. " + (object) listaDocumento1.TipoDocumento.Id + " duplicado";
//        //  }
//        //  List<clsTipoDocumento> clsTipoDocumentoList = new clsTipoDocumentoDAO(_conexao).Listar(_idTipoAgenda);
//        //  foreach (clsDocumento listaDocumento in this.ListaDocumentos)
//        //  {
//        //    foreach (clsTipoDocumento _TipoDocumento in clsTipoDocumentoList)
//        //    {
//        //      if (listaDocumento.TipoDocumento.Id == _TipoDocumento.Id)
//        //      {
//        //        if (string.IsNullOrEmpty(listaDocumento.Valor.Trim()))
//        //          str = str + "<br>- " + _TipoDocumento.Descricao + " Inválido";
//        //        else if (!clsTipoDocumento.ValidarDoc(_TipoDocumento, listaDocumento.Valor))
//        //          str = str + "<br>- " + _TipoDocumento.Descricao + " Inválido";
//        //      }
//        //    }
//        //  }
//        //  foreach (clsTipoDocumento clsTipoDocumento in clsTipoDocumentoList)
//        //  {
//        //    if (clsTipoDocumento.Obrigatorio)
//        //    {
//        //      bool flag = false;
//        //      foreach (clsDocumento listaDocumento in this.ListaDocumentos)
//        //      {
//        //        if (listaDocumento.TipoDocumento.Id == clsTipoDocumento.Id)
//        //          flag = true;
//        //      }
//        //      if (!flag)
//        //        str = str + "<br>- " + clsTipoDocumento.Descricao;
//        //    }
//        //  }
//        //  if (str != string.Empty)
//        //    throw new RegraNegocioException("<b>Campos Obrigatórios</b><br>" + str);
//        //}
//    }
//}
