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
            _nome = "informatica basica";
            _cargaHoraria = (double)80;
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = (double)950;
            _descricao = "descricao";
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
            var curso = new Curso(cursoEsperado.Nome,cursoEsperado.Descricao, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

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
    public enum PublicoAlvo
    {
        Estudante,
        Universitario,
        Empregado,
        Empreendedor
    }

    public class Curso
    {
        public string Nome { get; private set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }

        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("Nome invalido");
            }
            if (cargaHoraria < 1)
            {
                throw new ArgumentException("Carga horaria invalida");
            }

            if (valor < 1)
            {
                throw new ArgumentException("Valor invalido");
            }
            Nome = nome;
            CargaHoraria = cargaHoraria;
            PublicoAlvo = publicoAlvo;
            Valor = valor;
            Descricao = descricao;
        }
    }
}
