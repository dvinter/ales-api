
using Owin;
using System.Web.Http;

namespace agendaFacilAPI
{
    public class Server
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            config.Routes.MapHttpRoute(
                name: "Agenda Facil API",
                routeTemplate: "api/{controller}/{id}/{data}/{hora}/{dados}/{numeroDocumento}",
                defaults: new
                {
                    id = RouteParameter.Optional,
                    data = RouteParameter.Optional,
                    hora = RouteParameter.Optional,
                    dados = RouteParameter.Optional,
                    numeroDocumento = RouteParameter.Optional
                }
            );

            config.Routes.MapHttpRoute(
                    name: "ControllerAndAction",
                    routeTemplate: "api/{controller}/{action}"
            );

            //config.Filters.Add(new BasicAuthenticationAttribute());
            app.UseWebApi(config);
        }
    }
}

