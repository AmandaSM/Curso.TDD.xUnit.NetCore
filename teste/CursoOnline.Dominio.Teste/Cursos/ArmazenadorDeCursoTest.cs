using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Teste._Util;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
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

    }
    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);
    }

    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDTO cursoDTO)
        {
            Enum.TryParse(typeof(PublicoAlvo), cursoDTO.PublicoAlvo, out var publicoAlvo);//parseando(quest) e criando uma variavel
            if (publicoAlvo == null)
            {
                throw new ArgumentException("Publico alvo invalido");
            }

            var curso = new Curso(cursoDTO.Nome, cursoDTO.Descricao, cursoDTO.CargaHoraria,(PublicoAlvo) publicoAlvo,
                cursoDTO.Valor);
            _cursoRepositorio.Adicionar(curso);
        }
    }

    public class CursoDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }
    }
}


