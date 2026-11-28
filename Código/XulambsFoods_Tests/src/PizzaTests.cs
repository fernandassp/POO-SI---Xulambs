using Xunit;
using XulambsFoods_2024_2.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src.Tests {
    public class PizzaTests {
        Pizza pizza;

        public PizzaTests() {
            pizza = new Pizza();
        }

        [Fact]
        public void CriarPizzaVazia() {
            Assert.True(pizza.NotaDeCompra().Contains("0 ingredientes"));
        }

        [Fact]
        public void CriarPizzarComIngredientes() {
            pizza = new Pizza(2);
            Assert.True(pizza.NotaDeCompra().Contains("2 ingredientes"));
        }

        [Fact]
        public void CalculaValorCorretamente() {
            pizza.AdicionarIngredientes(2);
            Assert.Equal(39d, pizza.ValorFinal());
        }

        [Fact]
        public void AdicionaIngredientesCorretamente() {
            pizza = new Pizza();
            Assert.Equal(3, pizza.AdicionarIngredientes(3));
        }

        [Fact]
        public void NaoPodeAdicionarIngredientesAlemDoMaximo() {
            pizza = new Pizza(6);
            Assert.Equal(6, pizza.AdicionarIngredientes(3));
        }

        [Fact]
        public void GeraNotaDeCompraComDetalhes() {
            pizza = new Pizza(6);
            Assert.True(pizza.NotaDeCompra().Contains("6 ingredientes"));
        }
    }
}