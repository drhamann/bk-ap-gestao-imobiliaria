using Academia.Programador.Bk.Gestao.Imobiliaria.Dominio.ModuloCliente;
using Moq;

namespace Academia.Programador.Bk.Gestao.Imobiliaria.Tests.ModuloCliente;
public class ServiceClienteTests
{
    private readonly Mock<IClienteRepositorio> _mockRepositorio;
    private readonly ServiceCliente _serviceCliente;

    public ServiceClienteTests()
    {
        _mockRepositorio = new Mock<IClienteRepositorio>();
        _serviceCliente = new ServiceCliente(_mockRepositorio.Object, null);
    }

    [Fact]
    public void Dado_QueClienteNaoExiste_Quando_CriarCliente_Entao_ChamaRepositorio()
    {
        // Arrange
        var cliente = new Cliente { ClienteId = 1, Cpf = "12345678900", Email = "cliente@email.com" };

        _mockRepositorio.Setup(r => r.ClientePorCpf(cliente.Cpf, cliente.ClienteId)).Returns(false);
        _mockRepositorio.Setup(r => r.ClientePorEmail(cliente.Email, cliente.ClienteId)).Returns(false);

        // Act
        _serviceCliente.CriarCliente(cliente);

        // Assert
        _mockRepositorio.Verify(r => r.CriarCliente(cliente), Times.Once);
    }

    [Fact]
    public void Dado_QueClienteComMesmoCpfExiste_Quando_CriarCliente_Entao_LancaExcecao()
    {
        // Arrange
        var cliente = new Cliente { ClienteId = 1, Cpf = "12345678900", Email = "cliente@email.com" };

        _mockRepositorio.Setup(r => r.ClientePorCpf(cliente.Cpf, cliente.ClienteId)).Returns(true);

        // Act & Assert
        Assert.Throws<ClienteExistenteException>(() => _serviceCliente.CriarCliente(cliente));
        _mockRepositorio.Verify(r => r.CriarCliente(It.IsAny<Cliente>()), Times.Never);
    }

    [Fact]
    public void Dado_QueClienteComMesmoEmailExiste_Quando_CriarCliente_Entao_LancaExcecao()
    {
        // Arrange
        var cliente = new Cliente { ClienteId = 1, Cpf = "12345678900", Email = "cliente@email.com" };

        _mockRepositorio.Setup(r => r.ClientePorCpf(cliente.Cpf, cliente.ClienteId)).Returns(false);
        _mockRepositorio.Setup(r => r.ClientePorEmail(cliente.Email, cliente.ClienteId)).Returns(true);

        // Act & Assert
        Assert.Throws<ClienteExistenteException>(() => _serviceCliente.CriarCliente(cliente));
        _mockRepositorio.Verify(r => r.CriarCliente(It.IsAny<Cliente>()), Times.Never);
    }

    [Fact]
    public void Dado_QueClienteNaoExiste_Quando_SalvarCliente_Entao_ChamaRepositorio()
    {
        // Arrange
        var cliente = new Cliente { ClienteId = 1, Cpf = "12345678900", Email = "cliente@email.com" };

        _mockRepositorio.Setup(r => r.ClientePorCpf(cliente.Cpf, cliente.ClienteId)).Returns(false);
        _mockRepositorio.Setup(r => r.ClientePorEmail(cliente.Email, cliente.ClienteId)).Returns(false);

        // Act
        _serviceCliente.SalvarCliente(cliente);

        // Assert
        _mockRepositorio.Verify(r => r.SalvarCliente(cliente), Times.Once);
    }

    [Fact]
    public void Dado_QueClienteComMesmoCpfExiste_Quando_SalvarCliente_Entao_LancaExcecao()
    {
        // Arrange
        var cliente = new Cliente { ClienteId = 1, Cpf = "12345678900", Email = "cliente@email.com" };

        _mockRepositorio.Setup(r => r.ClientePorCpf(cliente.Cpf, cliente.ClienteId)).Returns(true);

        // Act & Assert
        Assert.Throws<ClienteExistenteException>(() => _serviceCliente.SalvarCliente(cliente));
        _mockRepositorio.Verify(r => r.SalvarCliente(It.IsAny<Cliente>()), Times.Never);
    }

    [Fact]
    public void Dado_QueClienteComMesmoEmailExiste_Quando_SalvarCliente_Entao_LancaExcecao()
    {
        // Arrange
        var cliente = new Cliente { ClienteId = 1, Cpf = "12345678900", Email = "cliente@email.com" };

        _mockRepositorio.Setup(r => r.ClientePorCpf(cliente.Cpf, cliente.ClienteId)).Returns(false);
        _mockRepositorio.Setup(r => r.ClientePorEmail(cliente.Email, cliente.ClienteId)).Returns(true);

        // Act & Assert
        Assert.Throws<ClienteExistenteException>(() => _serviceCliente.SalvarCliente(cliente));
        _mockRepositorio.Verify(r => r.SalvarCliente(It.IsAny<Cliente>()), Times.Never);
    }

    [Fact]
    public void Dado_QueClienteExiste_Quando_TragaClientePorId_Entao_RetornaCliente()
    {
        // Arrange
        var cliente = new Cliente { ClienteId = 1, Cpf = "12345678900", Email = "cliente@email.com" };
        _mockRepositorio.Setup(r => r.TragaClientePorId(1)).Returns(cliente);

        // Act
        var result = _serviceCliente.TragaClientePorId(1);

        // Assert
        Assert.Equal(cliente, result);
    }

    [Fact]
    public void Dado_QueClienteNaoExiste_Quando_TragaClientePorId_Entao_RetornaNull()
    {
        // Arrange
        Cliente nulo = null;
        _mockRepositorio.Setup(r => r.TragaClientePorId(1)).Returns(nulo);

        // Act
        var result = _serviceCliente.TragaClientePorId(1);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void Dado_QueClienteExiste_Quando_Remover_Entao_ChamaRepositorio()
    {
        // Arrange
        var clienteId = 1;

        // Act
        _serviceCliente.Remover(clienteId);

        // Assert
        _mockRepositorio.Verify(r => r.Remover(clienteId), Times.Once);
    }
}
