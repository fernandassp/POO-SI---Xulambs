using System.Collections.Generic;
using Xunit;

namespace XulambsFoods_2024_2.src.Tests
{
    public class OrdenadorTests
    {
        [Fact]
        public void ArrayVazioRetornaVazioSemErro()
        {
            var ord = new Ordenador<int>(new int[0]);
            int[] resultado = ord.ordenar();
            Assert.Empty(resultado);
        }

        [Fact]
        public void ArrayComUmItemPermanece()
        {
            var ord = new Ordenador<int>(new[] { 7 });
            Assert.Equal(new[] { 7 }, ord.ordenar());
        }

        [Fact]
        public void ArrayForaDeOrdemFicaCrescente()
        {
            var ord = new Ordenador<int>(new[] { 5, 1, 4, 2, 3 });
            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, ord.ordenar());
        }

        [Fact]
        public void ArrayJaOrdenadoMantemOrdem()
        {
            var ord = new Ordenador<int>(new[] { 1, 2, 3, 4, 5 });
            Assert.Equal(new[] { 1, 2, 3, 4, 5 }, ord.ordenar());
        }

        [Fact]
        public void ArrayComValoresRepetidosMantemTodos()
        {
            var ord = new Ordenador<int>(new[] { 3, 1, 3, 2, 1 });
            Assert.Equal(new[] { 1, 1, 2, 3, 3 }, ord.ordenar());
        }

        [Fact]
        public void ComparadorPersonalizadoOrdemDecrescente()
        {
            var comparador = Comparer<int>.Create((a, b) => b.CompareTo(a));
            var ord = new Ordenador<int>(new[] { 2, 5, 1, 4, 3 }, comparador);
            Assert.Equal(new[] { 5, 4, 3, 2, 1 }, ord.ordenar());
        }

        [Fact]
        public void ComparadorPersonalizadoPorTamanhoDaString()
        {
            var comparador = Comparer<string>.Create((a, b) => a.Length.CompareTo(b.Length));
            var ord = new Ordenador<string>(new[] { "ccc", "a", "bb" }, comparador);
            Assert.Equal(new[] { "a", "bb", "ccc" }, ord.ordenar());
        }
    }
}