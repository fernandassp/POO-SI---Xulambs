using Xunit;
using XulambsFoods_2024_2.src;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XulambsFoods_2024_2.src.Tests {
    public class SanduicheTests {
        Sanduiche sanduiche;

        public SanduicheTests() {
            sanduiche = new Sanduiche();
        }

        
        [Fact]
        public void CalcularValorFinal() {
            sanduiche.AdicionarIngredientes(3);
            Assert.Equal(24d, sanduiche.ValorFinal());
        }

        [Fact]
        public void ValorFinalComCombo() {
            sanduiche = new Sanduiche(true);
            sanduiche.AdicionarIngredientes(3);
            Assert.Equal(29d, sanduiche.ValorFinal());
        }


        [Fact]
        public void NotaDeCompraTest() {
            string nota = sanduiche.NotaDeCompra();
            Assert.True(nota.Contains("0 ingredientes") && 
                          (nota.Contains("15,00")));
        }
    }
}