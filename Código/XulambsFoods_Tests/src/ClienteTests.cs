using Xunit;
using XulambsFoods_2024_2.src;

namespace XulambsFoods_2024_2.src.Tests
{
    public class ClienteTests
    {
        private static Pedido PedidoComPizza(int ingredientes = 0)
        {
            var pedido = new Pedido(0);
            pedido.Adicionar(new Pizza(ingredientes));
            return pedido;
        }

        [Fact]
        public void ClienteComNomeNormalMantemNome()
        {
            var cliente = new Cliente("Maria");
            Assert.Contains("Maria", cliente.ToString());
        }

        [Fact]
        public void ClienteComNomeVazioRecebeNomeAnonimo()
        {
            var cliente = new Cliente("");
            Assert.Contains("Cliente anônimo", cliente.ToString());
        }

        [Fact]
        public void RegistrarPedidoRetornaQuantidadeAcumulada()
        {
            var cliente = new Cliente("Maria");
            Assert.Equal(1, cliente.RegistrarPedido(PedidoComPizza()));
            Assert.Equal(2, cliente.RegistrarPedido(PedidoComPizza()));
        }

        [Fact]
        public void TotalGastoSemPedidosEhZero()
        {
            var cliente = new Cliente("Maria");
            Assert.Equal(0d, cliente.TotalGasto());
        }

        [Fact]
        public void TotalGastoComUmPedido()
        {
            var cliente = new Cliente("Maria");
            var pedido = PedidoComPizza();
            cliente.RegistrarPedido(pedido);
            Assert.Equal(pedido.PrecoAPagar(), cliente.TotalGasto(), 0.01);
            Assert.Equal(31.9d, cliente.TotalGasto(), 0.01);
        }

        [Fact]
        public void TotalGastoComVariosPedidosSomaTodos()
        {
            var cliente = new Cliente("Maria");
            var p1 = PedidoComPizza();
            var p2 = PedidoComPizza(2);
            cliente.RegistrarPedido(p1);
            cliente.RegistrarPedido(p2);
            Assert.Equal(p1.PrecoAPagar() + p2.PrecoAPagar(), cliente.TotalGasto(), 0.01);
        }

        [Fact]
        public void RelatorioPedidosContemClienteETotal()
        {
            var cliente = new Cliente("Maria");
            cliente.RegistrarPedido(PedidoComPizza());
            string relatorio = cliente.RelatorioPedidos();
            Assert.Contains("Cliente Maria", relatorio);
            Assert.Contains("Total gasto pelo cliente", relatorio);
        }

        [Fact]
        public void FidelizarClienteAcrescentaNomeDoPrograma()
        {
            var cliente = new Cliente("Maria");
            cliente.fidelizarCliente("Gold");
            Assert.Contains("Maria Gold", cliente.ToString());
        }

        [Fact]
        public void CompareToOrdenaPorNome()
        {
            var ana = new Cliente("Ana");
            var bia = new Cliente("Bia");
            Assert.True(ana.CompareTo(bia) < 0);
            Assert.True(bia.CompareTo(ana) > 0);
        }

        [Fact]
        public void ClienteEhIgualASiMesmo()
        {
            var cliente = new Cliente("Maria");
            Assert.True(cliente.Equals(cliente));
        }

        [Fact]
        public void ClientesDiferentesComMesmoNomeNaoSaoIguais()
        {
            var a = new Cliente("Maria");
            var b = new Cliente("Maria");
            Assert.False(a.Equals(b));
        }

        [Fact]
        public void ClienteNaoEhIgualANulo()
        {
            Assert.False(new Cliente("Maria").Equals(null));
        }

        [Fact]
        public void ToStringContemNomeETotalGasto()
        {
            var cliente = new Cliente("Maria");
            cliente.RegistrarPedido(PedidoComPizza());
            string texto = cliente.ToString();
            Assert.Contains("Maria", texto);
            Assert.Contains("já gastou", texto);
        }
    }

    public class ComparadorDeGastosTests
    {
        private static Cliente ClienteComGasto(string nome, int ingredientes)
        {
            var cliente = new Cliente(nome);
            var pedido = new Pedido(0);
            pedido.Adicionar(new Pizza(ingredientes));
            cliente.RegistrarPedido(pedido);
            return cliente;
        }

        [Fact]
        public void ClienteQueGastouMaisEhMaior()
        {
            var comparador = new ComparadorDeGastos();
            var barato = ClienteComGasto("A", 0);
            var caro = ClienteComGasto("B", 3);
            Assert.Equal(1, comparador.Compare(caro, barato));
        }

        [Fact]
        public void ClienteQueGastouMenosEhMenor()
        {
            var comparador = new ComparadorDeGastos();
            var barato = ClienteComGasto("A", 0);
            var caro = ClienteComGasto("B", 3);
            Assert.Equal(-1, comparador.Compare(barato, caro));
        }

        [Fact]
        public void ClientesComMesmoGastoSaoIguaisNaComparacao()
        {
            var comparador = new ComparadorDeGastos();
            var a = ClienteComGasto("A", 2);
            var b = ClienteComGasto("B", 2);
            Assert.Equal(0, comparador.Compare(a, b));
        }
    }
}
