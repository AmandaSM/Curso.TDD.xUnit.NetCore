using Bogus;
using CursoOnline.Dominio.Teste._Builders;
using CursoOnline.Dominio.Teste._Util;
using Moq;
using System;
using Xunit;

namespace CursoOnline.Dominio.Teste.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private CursoDTO _cursoDTO;
        private Mock<ICursoRepositorio> _cursoRepositorioMock;
        private ArmazenadorDeCurso _armazenadorCurso;

        public ArmazenadorDeCursoTest()
        {
            var faker = new Faker();
            _cursoDTO = new CursoDTO
            {//OBJ transferencia
                Nome = faker.Random.Word(),
                Descricao = faker.Lorem.Paragraph(),
                CargaHoraria = faker.Random.Double(50.100),
                PublicoAlvo = "Estudante",
                Valor = faker.Random.Double(100.100)
                 
        };
            _cursoRepositorioMock = new Mock<ICursoRepositorio>();//valida se algum comportamento está funcionado ou n(ao chamalo)
            _armazenadorCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);// injentando atraves de um construtor

        }
        [Fact]
        public void DeveAdicionarCurso()
        {
            //DomainService
            _armazenadorCurso.Armazenar(_cursoDTO);
            _cursoRepositorioMock.Verify(r => r.Adicionar(It.Is<Curso>( c=> c.Nome == _cursoDTO.Nome
                && c.Descricao == _cursoDTO.Descricao
                && c.CargaHoraria == _cursoDTO.CargaHoraria
                && c.Valor == _cursoDTO.Valor
                )));
        }
        [Fact]
        public void NaoDeveAdicionarComPublicoAlvoInvalido()
        {
            var publicoAlvoInvalido = "Medico";
            _cursoDTO.PublicoAlvo = publicoAlvoInvalido;

            Assert.Throws<ArgumentException>(() => _armazenadorCurso.Armazenar(_cursoDTO))
            .ComMensagem("Publico Alvo Invalido");
        }
        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeJasalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDTO.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDTO.Nome)).Returns(cursoJaSalvo);
            Assert.Throws<ArgumentException>(() => _armazenadorCurso.Armazenar(_cursoDTO))
           .ComMensagem("Nome do curso já contem no banco de Dados ");
        }

    }
}
