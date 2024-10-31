using Academia.Programador.Bk.Gestao.Imobiliaria.DAO.Repositorios.EF;
using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web.Models;
using Academia.Programador.Bk.Gestao.Imobiliaria.Web.Views;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Tests.ModuloCliente
{
    public class ClientesIntegracaoTests
    {
        private ImobiliariaDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<ImobiliariaDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()) // Garantir que cada teste tenha um banco único
                .Options;

            var context = new ImobiliariaDbContext(options);
            SeedDatabase(context);
            return context;
        }

        private void SeedDatabase(ImobiliariaDbContext context)
        {
            var clientes = new List<Cliente>
            {
                new Cliente { ClienteId = 1, Nome = "Cliente 1", Cpf = "12345678900", Email = "cliente1@email.com" },
                new Cliente { ClienteId = 2, Nome = "Cliente 2", Cpf = "12345678901", Email = "cliente2@email.com" }
            };
            context.Clientes.AddRange(clientes);
            context.SaveChanges();
        }

        [Fact]
        public async Task Dado_QueHaClientes_Quando_ObterTodosClientes_Entao_RetornaViewComClientesViewModels()
        {
            // Arrange
            var context = CreateDbContext();
            var repositorio = new ClienteRepositorio(context);
            var service = new ServiceCliente(repositorio, null);
            var controller = new ClientesController(service);

            // Act
            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.NotNull(result);
            var viewModels = Assert.IsType<List<ClienteViewModel>>(result.Model);
            Assert.Equal(2, viewModels.Count);
        }

        [Fact]
        public async Task Dado_QueIdEhValido_Quando_ObterDetalhes_Entao_RetornaViewComClienteViewModel()
        {
            // Arrange
            var context = CreateDbContext();
            var repositorio = new ClienteRepositorio(context);
            var service = new ServiceCliente(repositorio, null);
            var controller = new ClientesController(service);

            // Act
            var result = await controller.Details(1) as ViewResult;

            // Assert
            Assert.NotNull(result);
            var viewModel = Assert.IsType<ClienteViewModel>(result.Model);
            Assert.Equal(1, viewModel.ClienteId);
            Assert.Equal("Cliente 1", viewModel.Nome);
        }

        [Fact]
        public async Task Dado_ModeloValido_Quando_CriarCliente_Entao_RedirecionaParaIndex()
        {
            // Arrange
            var context = CreateDbContext();
            var repositorio = new ClienteRepositorio(context);
            var service = new ServiceCliente(repositorio, null);
            var controller = new ClientesController(service);
            var clienteViewModel = new CreateClienteViewModel { Nome = "Novo Cliente", Cpf = "12345678902", Email = "novo@cliente.com" };

            // Act
            var result = await controller.Create(clienteViewModel) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            // Verify the client was added
            var cliente = context.Clientes.FirstOrDefault(c => c.Email == "novo@cliente.com");
            Assert.NotNull(cliente);
            Assert.Equal("Novo Cliente", cliente.Nome);
        }

        [Fact]
        public async Task Dado_QueIdEhValido_Quando_EditarCliente_Entao_RedirecionaParaIndex()
        {
            // Arrange
            var context = CreateDbContext();
            var repositorio = new ClienteRepositorio(context);
            var service = new ServiceCliente(repositorio, null);
            var controller = new ClientesController(service);
            var clienteViewModel = new ClienteViewModel { ClienteId = 1, Nome = "Cliente Atualizado" };

            // Act
            var result = await controller.Edit(1, clienteViewModel) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            // Verify the client was updated
            var cliente = context.Clientes.First(c => c.ClienteId == 1);
            Assert.Equal("Cliente Atualizado", cliente.Nome);
        }

        [Fact]
        public async Task Dado_QueIdEhValido_Quando_DeletarCliente_Entao_RedirecionaParaIndex()
        {
            // Arrange
            var context = CreateDbContext();
            var repositorio = new ClienteRepositorio(context);
            var service = new ServiceCliente(repositorio, null);
            var controller = new ClientesController(service);

            // Act
            var result = await controller.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Index", result.ActionName);

            // Verify the client was removed
            var cliente = context.Clientes.FirstOrDefault(c => c.ClienteId == 1);
            Assert.Null(cliente);
        }
    }
}
