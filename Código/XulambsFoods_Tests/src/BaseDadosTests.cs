using System;
using Xunit;

namespace XulambsFoods_2024_2.src.Tests
{
    public class BaseDadosTests
    {
        private const string Rodape = "==================";
        private static string NL => Environment.NewLine;

        [Fact]
        public void BaseVaziaTemQuantidadeZero()
        {
            var bd = new BaseDados<int>();
            Assert.Equal(0, bd.Quantidade());
        }

        [Fact]
        public void AddAumentaQuantidade()
        {
            var bd = new BaseDados<int>();
            bd.Add(10);
            bd.Add(20);
            Assert.Equal(2, bd.Quantidade());
        }

        [Fact]
        public void LocalizarItemExistenteRetornaItem()
        {
            var bd = new BaseDados<string>();
            string item = "pizza";
            bd.Add(item);
            Assert.Equal(item, bd.Localizar(item.GetHashCode()));
        }

        [Fact]
        public void LocalizarItemInexistenteRetornaNulo()
        {
            var bd = new BaseDados<string>();
            bd.Add("pizza");
            Assert.Null(bd.Localizar(-12345));
        }

        [Fact]
        public void TotalizadorRetornaSomaCorreta()
        {
            var bd = new BaseDados<int>();
            bd.Add(10);
            bd.Add(20);
            bd.Add(30);
            Assert.Equal(60d, bd.Totalizador(x => x), 2);
        }

        [Fact]
        public void ProcessarExecutaAcaoParaTodos()
        {
            var bd = new BaseDados<int>();
            bd.Add(1);
            bd.Add(2);
            bd.Add(3);
            int soma = 0, chamadas = 0;
            bd.Processar(x => { soma += x; chamadas++; });
            Assert.Equal(3, chamadas);
            Assert.Equal(6, soma);
        }

        [Fact]
        public void RelatorioOrdenadoPadraoEmOrdemCrescente()
        {
            var bd = new BaseDados<int>();
            bd.Add(30);
            bd.Add(10);
            bd.Add(20);
            string esperado = "10" + NL + "20" + NL + "30" + NL + Rodape;
            Assert.Equal(esperado, bd.RelatorioOrdenado());
        }

        [Fact]
        public void RelatorioOrdenadoComComparisonEmOrdemDecrescente()
        {
            var bd = new BaseDados<int>();
            bd.Add(10);
            bd.Add(30);
            bd.Add(20);
            string esperado = "30" + NL + "20" + NL + "10" + NL + Rodape;
            Assert.Equal(esperado, bd.RelatorioOrdenado((a, b) => b.CompareTo(a)));
        }
    }
}