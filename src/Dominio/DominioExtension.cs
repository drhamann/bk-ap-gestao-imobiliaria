using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCorretor;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloLogin;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloUsuario;
using Microsoft.Extensions.DependencyInjection;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Dominio
{
    public static class DominioExtension
    {
        //Metodo para controlar criação de objeto do IOC
        public static void AdicionarImplementacoesDominio(this IServiceCollection services)
        {
            services.AddTransient<IServiceCliente, ServiceCliente>();
            services.AddTransient<IServiceCorretor, ServiceCorretor>();
            services.AddTransient<IServiceImovel, ServiceImovel>();
            services.AddTransient<IServiceUsuario, ServiceUsuario>();
            services.AddTransient<ILoginService, LoginService>();
        }
    }
}
