using Microsoft.VisualStudio.TestTools.UnitTesting;
using XulambsFoods_2024_2.src;
using System;

namespace XulambsFoods_2024_2.src.Tests
{
    [TestClass()]
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

        [TestInitialize]
        public void SetUp()
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

        [TestMethod()]
        public void DistanciaMenorQue01DefinaTaxaZero()
        {
            // Arrange - já feito em SetUp
            // Act
            double taxa = pedidoMuItoPerto.ValorTaxa();
            // Assert
            Assert.AreEqual(0d, taxa);
        }

        [TestMethod()]
        public void DistanciaIgualA4DefinaTaxaZero()
        {
            // Arrange - já feito em SetUp
            // Act
            double taxa = pedidoDistancia4.ValorTaxa();
            // Assert
            Assert.AreEqual(0d, taxa);
        }

        [TestMethod()]
        public void DistanciaMaiorQue4EmenorQue8DefinaTaxaCinco()
        {
            // Arrange - já feito em SetUp
            // Act
            double taxa = pedidoDistancia6.ValorTaxa();
            // Assert
            Assert.AreEqual(5d, taxa);
        }

        [TestMethod()]
        public void DistanciaIgualA8DefinaTaxaCinco()
        {
            // Arrange - já feito em SetUp
            // Act
            double taxa = pedidoDistancia8.ValorTaxa();
            // Assert
            Assert.AreEqual(5d, taxa);
        }

        [TestMethod()]
        public void DistanciaMaiorQue8DefinaTaxaOito()
        {
            // Arrange - já feito em SetUp
            // Act
            double taxa = pedidoMuitoLonge.ValorTaxa();
            // Assert
            Assert.AreEqual(8d, taxa);
        }

        // ===== Testes de Adição de Comidas =====

        [TestMethod()]
        public void PedidoSemItensTemValorZero()
        {
            // Arrange - já feito em SetUp
            // Act
            double valor = pedidoDistancia4.ValorItens();
            // Assert
            Assert.AreEqual(0d, valor);
        }

        [TestMethod()]
        public void PedidoComUmaComidaRetornaQuantidade1()
        {
            // Arrange - já feito em SetUp
            // Act
            int quantidade = pedidoDistancia4.Adicionar(pizzaSemAdicionais);
            // Assert
            Assert.AreEqual(1, quantidade);
        }

        [TestMethod()]
        public void PedidoComDuasComidosRetornaQuantidade2()
        {
            // Arrange - já feito em SetUp
            // Act
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);
            int quantidade = pedidoDistancia4.Adicionar(pizzaCom2Adicionais);
            // Assert
            Assert.AreEqual(2, quantidade);
        }

        [TestMethod()]
        public void TentativaDeAdicionarTerceiraComidaLancaExcecao()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);
            pedidoDistancia4.Adicionar(pizzaCom2Adicionais);

            // Act & Assert
            Assert.ThrowsException<PedidoLotadoException>(
                () => pedidoDistancia4.Adicionar(pizzaCom5Adicionais));
        }

        // ===== Testes de Valor de Itens =====

        [TestMethod()]
        public void ValorItensComUmaPizzaSemAdicionais()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);

            // Act
            double valor = pedidoDistancia4.ValorItens();

            // Assert - Pizza sem adicionais custa 29.00
            Assert.AreEqual(29d, valor, 0.01);
        }

        [TestMethod()]
        public void ValorItensComDuasPizzas()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);
            pedidoDistancia4.Adicionar(pizzaCom2Adicionais);

            // Act
            double valor = pedidoDistancia4.ValorItens();

            // Assert - Pizza(29) + Pizza com 2 adicionais(29+10=39) = 68
            Assert.AreEqual(68d, valor, 0.01);
        }

        // ===== Testes de Relatório =====

        [TestMethod()]
        public void RelatorioContemPalavraENTREGA()
        {
            // Arrange - já feito em SetUp
            // Act
            string relatorio = pedidoDistancia4.Relatorio();

            // Assert
            Assert.IsTrue(relatorio.Contains("ENTREGA"));
        }

        [TestMethod()]
        public void RelatorioContemTaxaDeEntrega()
        {
            // Arrange - já feito em SetUp
            // Act
            string relatorio = pedidoDistancia4.Relatorio();

            // Assert
            Assert.IsTrue(relatorio.Contains("TAXA ENTREGA"));
        }

        [TestMethod()]
        public void RelatorioComUmaPizzaMostraDetalhes()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);

            // Act
            string relatorio = pedidoDistancia4.Relatorio();

            // Assert
            Assert.IsTrue(relatorio.Contains("Pizza"));
        }

        [TestMethod()]
        public void RelatorioComDuasPizzasMostraAmbas()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);
            pedidoDistancia4.Adicionar(pizzaCom2Adicionais);

            // Act
            string relatorio = pedidoDistancia4.Relatorio();

            // Assert
            // Deve aparecer as pizzas e a taxa
            Assert.IsTrue(relatorio.Contains("Pizza"));
            Assert.IsTrue(relatorio.Contains("TAXA ENTREGA"));
        }

        [TestMethod()]
        public void RelatorioFormatacaoComRodapeDeIguais()
        {
            // Arrange - já feito em SetUp
            // Act
            string relatorio = pedidoDistancia4.Relatorio();

            // Assert
            Assert.IsTrue(relatorio.Contains("========"));
        }

        // ===== Testes de Taxa para Diferentes Distâncias =====

        [TestMethod()]
        public void PedidoDistancia0_1DeveTerTaxaZero()
        {
            // Arrange
            PedidoEntrega pedido = new PedidoEntrega(0.1);

            // Act
            double taxa = pedido.ValorTaxa();

            // Assert
            Assert.AreEqual(0d, taxa);
        }

        [TestMethod()]
        public void PedidoDistancia4_5DeveTerTaxaCinco()
        {
            // Arrange
            PedidoEntrega pedido = new PedidoEntrega(4.5);

            // Act
            double taxa = pedido.ValorTaxa();

            // Assert
            Assert.AreEqual(5d, taxa);
        }

        [TestMethod()]
        public void PedidoDistancia7_99DeveTerTaxaCinco()
        {
            // Arrange
            PedidoEntrega pedido = new PedidoEntrega(7.99);

            // Act
            double taxa = pedido.ValorTaxa();

            // Assert
            Assert.AreEqual(5d, taxa);
        }

        [TestMethod()]
        public void PedidoDistancia100DeveTerTaxaOito()
        {
            // Arrange
            PedidoEntrega pedido = new PedidoEntrega(100);

            // Act
            double taxa = pedido.ValorTaxa();

            // Assert
            Assert.AreEqual(8d, taxa);
        }

        // ===== Testes de Comportamento da Exceção =====

        [TestMethod()]
        [ExpectedException(typeof(PedidoLotadoException))]
        public void ExcecaoPedidoLotadoContemmensagem()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);
            pedidoDistancia4.Adicionar(pizzaCom2Adicionais);

            // Act
            pedidoDistancia4.Adicionar(pizzaCom5Adicionais);
        }

        [TestMethod()]
        public void ExcecaoPedidoLotadoLancadaNaTerceiraAdicao()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);
            pedidoDistancia4.Adicionar(pizzaCom2Adicionais);

            // Act & Assert
            try
            {
                pedidoDistancia4.Adicionar(pizzaCom5Adicionais);
                Assert.Fail("Deveria ter lançado PedidoLotadoException");
            }
            catch (PedidoLotadoException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Máximo"));
            }
        }

        // ===== Testes de Integração Distância e Taxa =====

        [TestMethod()]
        public void PedidoComDistancia1km()
        {
            // Arrange
            PedidoEntrega pedido = new PedidoEntrega(1);

            // Act
            double taxa = pedido.ValorTaxa();

            // Assert
            Assert.AreEqual(0d, taxa);
        }

        [TestMethod()]
        public void PedidoComDistancia5km()
        {
            // Arrange
            PedidoEntrega pedido = new PedidoEntrega(5);

            // Act
            double taxa = pedido.ValorTaxa();

            // Assert
            Assert.AreEqual(5d, taxa);
        }

        [TestMethod()]
        public void PedidoComDistancia9km()
        {
            // Arrange
            PedidoEntrega pedido = new PedidoEntrega(9);

            // Act
            double taxa = pedido.ValorTaxa();

            // Assert
            Assert.AreEqual(8d, taxa);
        }

        // ===== Testes de Comidas Diferentes =====

        [TestMethod()]
        public void AdicionaSanduicheEmPedidoEntrega()
        {
            // Arrange - já feito em SetUp
            // Act
            int quantidade = pedidoDistancia4.Adicionar(sanduicheSemAdicionais);

            // Assert
            Assert.AreEqual(1, quantidade);
        }

        [TestMethod()]
        public void PedidoComPizzaESanduiche()
        {
            // Arrange - já feito em SetUp
            pedidoDistancia4.Adicionar(pizzaSemAdicionais);

            // Act
            int quantidade = pedidoDistancia4.Adicionar(sanduicheSemAdicionais);

            // Assert
            Assert.AreEqual(2, quantidade);
        }

        // ===== Testes de Comportamento com Comida Nula =====

        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AdicionarComidaNulaLancaExcecao()
        {
            // Arrange - já feito em SetUp
            // Act
            pedidoDistancia4.Adicionar(null);
        }
    }
}
