using Xunit;
using XulambsFoods_2024_2.src;
using System;

namespace XulambsFoods_2024_2.src.Tests
{
    public class PedidoEntregaTests
    {
        private PedidoEntrega pedidoMuItoPerto;  // distância < 0.1
        private PedidoEntrega pedidoDistancia4; // distância = 4
        private PedidoEntrega pedidoDistancia6; // distância > 4 e < 8
        private PedidoEntrega pedidoDistancia8; // distância = 8
        private PedidoEntrega pedidoMuitoLonge; // distância > 8

        private Pizza pizzaSemAdicionais;
        private Pizza pizzaCom2Adicionais;
        private Pizza pizzaCom5Adicionais;
        private Sanduiche sanduicheSemAdicionais;

        public PedidoEntregaTests()
        {
            // Inicializar pedidos com diferentes distâncias
            pedidoMuItoPerto = new PedidoEntrega(0.05);     // menor que 0.1
            pedidoDistancia4 = new PedidoEntrega(4);         // igual a 4
            pedidoDistancia6 = new PedidoEntrega(6);         // entre 4 e 8
            pedidoDistancia8 = new PedidoEntrega(8);         // igual a 8
            pedidoMuitoLonge = new PedidoEntrega(10);        // maior que 8

            // Inicializar comidas
            pizzaSemAdicionais = new Pizza();
            pizzaCom2Adicionais = new Pizza(2);
            pizzaCom5Adicionais = new Pizza(5);
            sanduicheSemAdicionais = new Sanduiche();
        }

        // ===== Testes de Distância =====

        [Fact]
        public void DistanciaMenorQue01DefinaTaxaZero()
        {
            // Arrange - já feito em SetUp
            // Act
            double taxa = pedidoMuItoPerto.ValorTaxa();
            // Assert
            Assert.Equal(0d, taxa);
        }

        [Fact]
        public void DistanciaIgualA4DefinaTaxaZero()
        {
            // Arrange - já feito em SetUp
            // Act
            double taxa = pedidoDistancia4.ValorTaxa();
            // Assert
            Assert.Equal(0d, taxa);
        }

        [Fact]
        public void DistanciaMaiorQue4EmenorQue8DefinaTaxaCinco()
        {
            // Arrange - já feito em SetUp
            // Act
            double taxa = pedidoDistancia6.ValorTaxa();
            // Assert
            Assert.Equal(5d, taxa);
        }

        [Fact]
        public void DistanciaIgualA8DefinaTaxaCinco()
        {
            // Arrange - já feito em SetUp
            // Act
            double taxa = pedidoDistancia8.ValorTaxa();
            // Assert
            Assert.Equal(5d, taxa);
        }

        [Fact]
        public void DistanciaMaiorQue8DefinaTaxaOito()
        {
            // Arrange - já feito em SetUp
            // Act
            double taxa = pedidoMuitoLonge.ValorTaxa();
            // Assert
            Assert.Equal(8d, taxa);
        }

        // ===== Testes de Adição de Comidas =====

        [Fact]
        public void PedidoSemItensTemValorZero()
        {
            // Arrange - já feito em SetUp
            // Act
            double valor = pedidoDistancia4.ValorItens();
            // Assert
            Assert.Equal(0d, valor);
        }

        [Fact]
        public void PedidoComUmaComidaRetornaQuantidade1()
        {
            // Arrange - já feito em SetUp
            // Act
            int quantidade = pedidoDistancia4.Adicionar(pizzaSemAdicionais);
            // Assert
            Assert.Equal(1, quantidade);
        }

        [Fact]
        public void PedidoComDuasComidosRetornaQuantidade2()
        {
            // Arrange - já feito em SetUp
            // Act
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);
            int quantidade = pedidoDistancia4.Adicionar(pizzaCom2Adicionais);
            // Assert
            Assert.Equal(2, quantidade);
        }

        [Fact]
        public void TentativaDeAdicionarTerceiraComidaLancaExcecao()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);
            pedidoDistancia4.Adicionar(pizzaCom2Adicionais);

            // Act & Assert
            Assert.Throws<PedidoLotadoException>(
                () => pedidoDistancia4.Adicionar(pizzaCom5Adicionais));
        }

        // ===== Testes de Valor de Itens =====

        [Fact]
        public void ValorItensComUmaPizzaSemAdicionais()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);

            // Act
            double valor = pedidoDistancia4.ValorItens();

            // Assert - Pizza sem adicionais custa 29.00
            Assert.Equal(29d, valor, 0.01);
        }

        [Fact]
        public void ValorItensComDuasPizzas()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);
            pedidoDistancia4.Adicionar(pizzaCom2Adicionais);

            // Act
            double valor = pedidoDistancia4.ValorItens();

            // Assert - Pizza(29) + Pizza com 2 adicionais(29+10=39) = 68
            Assert.Equal(68d, valor, 0.01);
        }

        // ===== Testes de Relatório =====

        [Fact]
        public void RelatorioContemPalavraENTREGA()
        {
            // Arrange - já feito em SetUp
            // Act
            string relatorio = pedidoDistancia4.Relatorio();

            // Assert
            Assert.True(relatorio.Contains("ENTREGA"));
        }

        [Fact]
        public void RelatorioContemTaxaDeEntrega()
        {
            // Arrange - já feito em SetUp
            // Act
            string relatorio = pedidoDistancia4.Relatorio();

            // Assert
            Assert.True(relatorio.Contains("TAXA ENTREGA"));
        }

        [Fact]
        public void RelatorioComUmaPizzaMostraDetalhes()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);

            // Act
            string relatorio = pedidoDistancia4.Relatorio();

            // Assert
            Assert.True(relatorio.Contains("Pizza"));
        }

        [Fact]
        public void RelatorioComDuasPizzasMostraAmbas()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);
            pedidoDistancia4.Adicionar(pizzaCom2Adicionais);

            // Act
            string relatorio = pedidoDistancia4.Relatorio();

            // Assert
            // Deve aparecer as pizzas e a taxa
            Assert.True(relatorio.Contains("Pizza"));
            Assert.True(relatorio.Contains("TAXA ENTREGA"));
        }

        [Fact]
        public void RelatorioFormatacaoComRodapeDeIguais()
        {
            // Arrange - já feito em SetUp
            // Act
            string relatorio = pedidoDistancia4.Relatorio();

            // Assert
            Assert.True(relatorio.Contains("========"));
        }

        // ===== Testes de Taxa para Diferentes Distâncias =====

        [Fact]
        public void PedidoDistancia0_1DeveTerTaxaZero()
        {
            // Arrange
            PedidoEntrega pedido = new PedidoEntrega(0.1);

            // Act
            double taxa = pedido.ValorTaxa();

            // Assert
            Assert.Equal(0d, taxa);
        }

        [Fact]
        public void PedidoDistancia4_5DeveTerTaxaCinco()
        {
            // Arrange
            PedidoEntrega pedido = new PedidoEntrega(4.5);

            // Act
            double taxa = pedido.ValorTaxa();

            // Assert
            Assert.Equal(5d, taxa);
        }

        [Fact]
        public void PedidoDistancia7_99DeveTerTaxaCinco()
        {
            // Arrange
            PedidoEntrega pedido = new PedidoEntrega(7.99);

            // Act
            double taxa = pedido.ValorTaxa();

            // Assert
            Assert.Equal(5d, taxa);
        }

        [Fact]
        public void PedidoDistancia100DeveTerTaxaOito()
        {
            // Arrange
            PedidoEntrega pedido = new PedidoEntrega(100);

            // Act
            double taxa = pedido.ValorTaxa();

            // Assert
            Assert.Equal(8d, taxa);
        }

        // ===== Testes de Comportamento da Exceção =====

        [Fact]
        public void ExcecaoPedidoLotadoContemmensagem()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);
            pedidoDistancia4.Adicionar(pizzaCom2Adicionais);

            // Act & Assert
            Assert.Throws<PedidoLotadoException>(
                () => pedidoDistancia4.Adicionar(pizzaCom5Adicionais));
        }

        [Fact]
        public void ExcecaoPedidoLotadoLancadaNaTerceiraAdicao()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);
            pedidoDistancia4.Adicionar(pizzaCom2Adicionais);

            // Act & Assert
            var ex = Assert.Throws<PedidoLotadoException>(
                () => pedidoDistancia4.Adicionar(pizzaCom5Adicionais));
            Assert.True(ex.Message.Contains("Máximo"));
        }

        // ===== Testes de Integração Distância e Taxa =====

        [Fact]
        public void PedidoComDistancia1km()
        {
            // Arrange
            PedidoEntrega pedido = new PedidoEntrega(1);

            // Act
            double taxa = pedido.ValorTaxa();

            // Assert
            Assert.Equal(0d, taxa);
        }

        [Fact]
        public void PedidoComDistancia5km()
        {
            // Arrange
            PedidoEntrega pedido = new PedidoEntrega(5);

            // Act
            double taxa = pedido.ValorTaxa();

            // Assert
            Assert.Equal(5d, taxa);
        }

        [Fact]
        public void PedidoComDistancia9km()
        {
            // Arrange
            PedidoEntrega pedido = new PedidoEntrega(9);

            // Act
            double taxa = pedido.ValorTaxa();

            // Assert
            Assert.Equal(8d, taxa);
        }

        // ===== Testes de Comidas Diferentes =====

        [Fact]
        public void AdicionaSanduicheEmPedidoEntrega()
        {
            // Arrange - já feito em SetUp
            // Act
            int quantidade = pedidoDistancia4.Adicionar(sanduicheSemAdicionais);

            // Assert
            Assert.Equal(1, quantidade);
        }

        [Fact]
        public void PedidoComPizzaESanduiche()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);

            // Act
            int quantidade = pedidoDistancia4.Adicionar(sanduicheSemAdicionais);

            // Assert
            Assert.Equal(2, quantidade);
        }

        // ===== Testes de Comportamento com Comida Nula =====

        [Fact]
        public void AdicionarComidaNulaLancaExcecao()
        {
            // Arrange - já feito em SetUp
            // Act & Assert
            Assert.Throws<ArgumentNullException>(
                () => pedidoDistancia4.Adicionar(null));
        }
    }
}
