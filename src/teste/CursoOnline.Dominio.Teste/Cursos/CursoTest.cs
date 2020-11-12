using Bogus;
using CursoOnline.Dominio.Cursos;
using CursoOnline.Dominio.Teste._Builders;
using CursoOnline.Dominio.Teste._Util;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Dominio.Teste.Cursos
{
    //" Eu enquanto o administrador, quero criar e editar cursos para que sejam abertas matriculas paraq o mesmo."
    //- Critérios de aceite
    //- Criar um curso com nome, carga horária, publico alvo e valor curso.
    //- As opções para publico alvo são: Universitario, Estudante, Empregado e Empreendedor
    //- Todos os campos do curso são obrigátorios
    //- Curso deve ter uma descrição

    public class CursoTest : IDisposable
    {
        public readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor sendo Executado");
            var faker = new Faker();

            _nome = faker.Random.Word();
            _cargaHoraria = faker.Random.Double(50.100);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Double(100.100);
            _descricao = faker.Lorem.Paragraph();
        }
        public void Dispose()
        {
            _output.WriteLine("Dispose sendo Executado");
        }

        [Fact]
        public void DeveCriarCurso()
        {
            //ORGANIZAÇÃO
            var cursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao
            };

            //AÇÃO
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

            //ASSERT
            cursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerNomeInvalido(string nomeInvalido)
        {
            //ASSERT
            Assert.Throws<ArgumentException>(() =>
                 CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                  .ComMensagem("Nome invalido");
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerDescricaoInvalido(string descricaoInvalido)
        {
            //ASSERT
            Assert.Throws<ArgumentException>(() =>
                 CursoBuilder.Novo().ComNome(descricaoInvalido).Build())
                  .ComMensagem("Descricao invalida");
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void NaoDeveTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {
            //ASSERT
            Assert.Throws<ArgumentException>(() =>
            CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                .ComMensagem("Carga horaria invalida");

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void NaoDeveTerValorMenorQue1(double valorInvalido)
        {
            //ASSERT
            Assert.Throws<ArgumentException>(() =>
                CursoBuilder.Novo().ComValor(valorInvalido).Build())
           .ComMensagem("Valor invalido");
        }


    }
}

