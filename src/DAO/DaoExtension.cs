using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF;
using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF.Modulo_Corretor;
using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF.Modulo_Imovel;
using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF.ModuloUsuario;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCorretor;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloImovel;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloUsuario;
using Microsoft.Extensions.DependencyInjection;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.DAO
{
    public static class DaoExtension
    {
        //Metodo para controlar criação de objeto do IOC
        public static void AdicionarImplementacoesDeDados(this IServiceCollection services)
        {
            services.AddTransient<IClienteRepositorio, ClienteRepositorio>();
            services.AddTransient<ICorretorRepositorio, CorretorRepositorio>();
            services.AddTransient<IImovelRepositorio, ImovelRepositorio>();
            services.AddTransient<IUsuarioRepositorio, UsuarioRepositorio>();
        }
    }
}
