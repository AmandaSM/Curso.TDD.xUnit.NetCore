using Xunit;

namespace CursoOnline.Dominio.Teste
{
    public class UnitTest1
    {
        [Fact(DisplayName ="Test")]//NameMyTest
        public void DeveAsVaraveisTerremOMesmoValor()
        {
            //3A
            //Organizacao
            var variave1 = 1;
            var variave2 = 1;

            //ação
            variave1 = variave2;

            //assert
            Assert.Equal(variave1, variave2);
        }
    }
}
