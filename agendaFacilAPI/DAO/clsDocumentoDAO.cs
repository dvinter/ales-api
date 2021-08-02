//// Decompiled with JetBrains decompiler
//// Type: Intranet.SAGE.RegraNegocio.clsDocumentoDAO
//// Assembly: Intranet.SAGE.RegraNegocio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: E983EC8F-1008-4D0E-AFF6-43BA4207D9C3
//// Assembly location: C:\Users\davi.vinter.INTELLI019\Downloads\Intranet.SAGE.RegraNegocio.dll

//using agendaFacilAPI.DAO;
//using System;
//using System.Collections.Generic;
//using System.Data.SqlClient;
//using System.Linq;

//namespace Intranet.SAGE.RegraNegocio
//{
//  public class clsDocumentoDAO
//  {
//    private string Conexao { get; set; }

//    public clsDocumentoDAO(string _conexao)
//    {
//      this.Conexao = _conexao;
//    }

//    public List<clsDocumento> ListarViaAtendimento(clsAgendaAtendimentoDecompilado _atendimento)
//    {
//      List<clsDocumento> clsDocumentoList = new List<clsDocumento>();
//      using (SqlConnection connection = new SqlConnection(this.Conexao))
//      {
//        string cmdText = " SELECT SAGE_DOCUMENTO.*, SAGE_TIPO_DOCUMENTO.TX_TIPO_DOCUMENTO, SAGE_TIPO_DOCUMENTO.IC_SITUACAO, SAGE_DOCUMENTO_EXIGIDO.IC_OBRIGATORIO " + Environment.NewLine + " FROM SAGE_DOCUMENTO " + Environment.NewLine + " JOIN SAGE_TIPO_DOCUMENTO " + Environment.NewLine + " ON\tSAGE_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO = SAGE_DOCUMENTO.ID_TIPO_DOCUMENTO " + Environment.NewLine + " JOIN SAGE_DOCUMENTO_EXIGIDO " + Environment.NewLine + " ON\tSAGE_TIPO_DOCUMENTO.ID_TIPO_DOCUMENTO = SAGE_DOCUMENTO_EXIGIDO.ID_TIPO_DOCUMENTO " + Environment.NewLine + " WHERE SAGE_DOCUMENTO.ID_AGENDA_ATENDIMENTO = @IdAgendaAtendimento " + Environment.NewLine + " ORDER BY SAGE_DOCUMENTO_EXIGIDO.NR_ORDEM_EXIBICAO,  SAGE_TIPO_DOCUMENTO.TX_TIPO_DOCUMENTO ";
//        SqlDataReader sqlDataReader = (SqlDataReader) null;
//        try
//        {
//          connection.Open();
//          SqlCommand sqlCommand = new SqlCommand(cmdText, connection);
//          sqlCommand.Parameters.AddWithValue("@IdAgendaAtendimento", (object) _atendimento.Id);
//          using (sqlDataReader = sqlCommand.ExecuteReader())
//          {
//            while (sqlDataReader.Read())
//            {
//              clsTipoDocumento clsTipoDocumento = new clsTipoDocumento()
//              {
//                Id = Convert.ToInt32(sqlDataReader["ID_TIPO_DOCUMENTO"]),
//                Descricao = sqlDataReader["TX_TIPO_DOCUMENTO"].ToString(),
//                EstahAtivo = sqlDataReader["IC_SITUACAO"].ToString().Equals("A"),
//                Obrigatorio = sqlDataReader["IC_OBRIGATORIO"].ToString().Equals("S")
//              };
//              clsDocumentoList.Add(new clsDocumento()
//              {
//                TipoDocumento = clsTipoDocumento,
//                AgendaAtendimento = _atendimento,
//                Id = Convert.ToInt32(sqlDataReader["ID_DOCUMENTO"]),
//                Valor = sqlDataReader["TX_DOCUMENTO"].ToString()
//              });
//            }
//          }
//        }
//        catch (Exception ex)
//        {
//          throw ex;
//        }
//        finally
//        {
//          sqlDataReader?.Close();
//          connection?.Close();
//        }
//      }
//      return clsDocumentoList;
//    }

//    public List<clsDocumento> ListarViaTipoAgenda(int _IdTipoAgenda)
//    {
//      List<clsDocumento> clsDocumentoList = new List<clsDocumento>();
//      try
//      {
//        foreach (clsTipoDocumento clsTipoDocumento in new clsTipoDocumentoDAO(this.Conexao).Listar(_IdTipoAgenda).Where<clsTipoDocumento>((Func<clsTipoDocumento, bool>) (x => x.EstahAtivo)).ToList<clsTipoDocumento>())
//          clsDocumentoList.Add(new clsDocumento()
//          {
//            TipoDocumento = clsTipoDocumento,
//            AgendaAtendimento = new clsAgendaAtendimento(),
//            Id = 0,
//            Valor = string.Empty
//          });
//      }
//      catch (Exception ex)
//      {
//        throw ex;
//      }
//      return clsDocumentoList;
//    }

//    public void Inserir(clsDocumento _Documento, SqlConnection _conexao, SqlTransaction _trans)
//    {
//      string cmdText = " INSERT INTO SAGE_DOCUMENTO (ID_AGENDA_ATENDIMENTO, ID_TIPO_DOCUMENTO, TX_DOCUMENTO) VALUES(@ID_AGENDA_ATENDIMENTO, @ID_TIPO_DOCUMENTO, @TX_DOCUMENTO)";
//      try
//      {
//        SqlCommand sqlCommand = new SqlCommand(cmdText, _conexao, _trans);
//        sqlCommand.Parameters.AddWithValue("@ID_AGENDA_ATENDIMENTO", (object) _Documento.AgendaAtendimento.Id);
//        sqlCommand.Parameters.AddWithValue("@ID_TIPO_DOCUMENTO", (object) _Documento.TipoDocumento.Id);
//        sqlCommand.Parameters.AddWithValue("@TX_DOCUMENTO", (object) _Documento.Valor);
//        sqlCommand.ExecuteNonQuery();
//      }
//      catch (Exception ex)
//      {
//        throw ex;
//      }
//    }

//    public void Atualizar(clsDocumento _Documento, SqlConnection _conexao, SqlTransaction _trans)
//    {
//      string cmdText = " UPDATE SAGE_DOCUMENTO  " + Environment.NewLine + " SET ID_TIPO_DOCUMENTO = @ID_TIPO_DOCUMENTO,  " + Environment.NewLine + " TX_DOCUMENTO = @TX_DOCUMENTO " + Environment.NewLine + " WHERE ID_DOCUMENTO = @ID  ";
//      try
//      {
//        SqlCommand sqlCommand = new SqlCommand(cmdText, _conexao, _trans);
//        sqlCommand.Parameters.AddWithValue("@ID", (object) _Documento.Id);
//        sqlCommand.Parameters.AddWithValue("@ID_TIPO_DOCUMENTO", (object) _Documento.TipoDocumento.Id);
//        sqlCommand.Parameters.AddWithValue("@TX_DOCUMENTO", (object) _Documento.Valor);
//        sqlCommand.ExecuteNonQuery();
//      }
//      catch (Exception ex)
//      {
//        throw ex;
//      }
//    }
//  }
//}
