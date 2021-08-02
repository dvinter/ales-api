//// Decompiled with JetBrains decompiler
//// Type: Intranet.SAGE.RegraNegocio.clsDocumento
//// Assembly: Intranet.SAGE.RegraNegocio, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
//// MVID: E983EC8F-1008-4D0E-AFF6-43BA4207D9C3
//// Assembly location: C:\Users\davi.vinter.INTELLI019\Downloads\Intranet.SAGE.RegraNegocio.dll

//using agendaFacilAPI.DAO;

//namespace Intranet.SAGE.RegraNegocio
//{
//  public class clsDocumento
//  {
//    private string valor = string.Empty;

//    public clsDocumento()
//    {
//      this.TipoDocumento = new clsTipoDocumento();
//      this.AgendaAtendimento = new clsAgendaAtendimento();
//    }

//    public clsTipoDocumento TipoDocumento { get; set; }

//    public clsAgendaAtendimento AgendaAtendimento { get; set; }

//    public int Id { get; set; }

//    public string Valor
//    {
//      get
//      {
//        return this.valor;
//      }
//      set
//      {
//        this.valor = clsTipoDocumento.RetirarMascara(this.TipoDocumento, value);
//      }
//    }

//    public string ValorComMascara
//    {
//      get
//      {
//        if (string.IsNullOrEmpty(this.valor))
//          this.valor = string.Empty;
//        this.valor = clsTipoDocumento.RetirarMascara(this.TipoDocumento, this.valor);
//        return clsTipoDocumento.ColocarMascara(this.TipoDocumento, this.valor);
//      }
//      set
//      {
//        this.valor = value;
//      }
//    }
//  }
//}
