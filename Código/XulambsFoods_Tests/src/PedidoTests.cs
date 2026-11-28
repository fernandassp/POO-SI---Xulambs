using Xunit;
using XulambsFoods_2024_2.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src.Tests {
    public class PedidoTests {
        Pedido pedido;
        Pizza pizzaCom2Ingredientes;
        Pizza pizzaSemIngredientes;

        public PedidoTests() {
            pizzaCom2Ingredientes = new Pizza(2);
            pizzaSemIngredientes = new Pizza();
            pedido = new Pedido(0);
        }

        [Fact]
        public void AdicionaPizzaCorretamente() {
            Assert.Equal(1, pedido.Adicionar(pizzaCom2Ingredientes));
        }

        [Fact]
        public void naoAdicionaPizzaEmPedidoFechado() {
            pedido.Adicionar(pizzaCom2Ingredientes);
            pedido.FecharPedido();
            Assert.Throws<InvalidOperationException>(
                () => pedido.Adicionar(pizzaSemIngredientes));
        }

        [Fact]
        public void calculaPrecoCorretamente() {
            pedido.Adicionar(pizzaCom2Ingredientes);
            pedido.Adicionar(pizzaSemIngredientes);
            Assert.Equal(74.8d, pedido.PrecoAPagar(), 0.01);
        }

        [Fact]
        public void relatorioContemDetalhes() {
            pedido.Adicionar(pizzaCom2Ingredientes);
            pedido.Adicionar(pizzaSemIngredientes);
            string relatorio = pedido.Relatorio();
            Assert.True(relatorio.Contains("29,00"));
            Assert.True(relatorio.Contains("39,00"));
            Assert.True(relatorio.Contains("74,80"));
        }

        [Fact]
        public void rejeitaComidaNula() {
            Assert.Throws<ArgumentNullException>(
                () => pedido.Adicionar(null));
        }

        [Fact]
        public void pedidoVazioContinuaAbertoAposTentativaDeFechamento() {
            pedido.FecharPedido();

            Assert.Equal(1, pedido.Adicionar(pizzaSemIngredientes));
        }

        [Fact]
        public void criaPedidoParaEntregaQuandoDistanciaEhPositiva() {
            Pedido pedidoEntrega = new Pedido(5);

            pedidoEntrega.Adicionar(pizzaSemIngredientes);

            Assert.True(pedidoEntrega.Relatorio().Contains("ENTREGA"));
            Assert.Equal(34d, pedidoEntrega.PrecoAPagar(), 0.01);
        }

        [Fact]
        public void criaPedidoLocalQuandoDistanciaEhZero() {
            Assert.True(pedido.Relatorio().Contains("LOCAL"));
        }

        [Fact]
        public void toStringRetornaRelatorioDoPedido() {
            Assert.Equal(pedido.Relatorio(), pedido.ToString());
        }

        [Fact]
        public void pedidoEhIgualAoProprioIdentificador() {
            Assert.True(pedido.Equals(pedido.GetHashCode()));
        }

        [Fact]
        public void pedidosDiferentesNaoSaoIguais() {
            Pedido outroPedido = new Pedido(0);

            Assert.False(pedido.Equals(outroPedido));
        }

        [Fact]
        public void comparaPedidoMaisBaratoComPedidoMaisCaro() {
            Pedido pedidoMaisCaro = new Pedido(0);
            pedidoMaisCaro.Adicionar(pizzaCom2Ingredientes);

            pedido.Adicionar(pizzaSemIngredientes);

            Assert.Equal(-1, pedido.CompareTo(pedidoMaisCaro));
            Assert.Equal(1, pedidoMaisCaro.CompareTo(pedido));
        }

        [Fact]
        public void comparaPedidosComMesmoPreco() {
            Pedido outroPedido = new Pedido(0);

            Assert.Equal(0, pedido.CompareTo(outroPedido));
        }

        [Fact]
        public void pedidoFechadoNaoPodeSerAlterado() {
            pedido.Adicionar(pizzaSemIngredientes);
            pedido.FecharPedido();

            Assert.Throws<InvalidOperationException>(
                () => pedido.Adicionar(pizzaCom2Ingredientes));
        }
    }
}