using CursoOnline.Dominio.Teste._Util;
using ExpectedObjects;
using System;
using Xunit;
using Xunit.Abstractions;

namespace CursoOnline.Dominio.Teste.Cursos
{
    public class CursoTest : IDisposable
    {
        public readonly ITestOutputHelper _output;
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;

        public CursoTest(ITestOutputHelper output)
        {
            _output = output;
            _output.WriteLine("Construtor sendo Executado");
            _nome = "informatica basica";
            _cargaHoraria = (double)80;
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = (double)950;
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
                Valor = _valor
            };

            //AÇÃO
            var curso = new Curso(cursoEsperado.Nome, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor);

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
                  new Curso(nomeInvalido, _cargaHoraria, _publicoAlvo, _valor))
                  .ComMensagem("Nome invalido");
        }


        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void NaoDeveTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {
            //ASSERT
            Assert.Throws<ArgumentException>(() =>
            new Curso(_nome, cargaHorariaInvalida, _publicoAlvo, _valor))
                .ComMensagem("Carga horaria invalida");

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void NaoDeveTerValorMenorQue1(double valorInvalido)
        {
            //ASSERT
            Assert.Throws<ArgumentException>(() =>
           new Curso(_nome, valorInvalido, _publicoAlvo, _valor))
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
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }

        public Curso(string nome, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
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
        }
    }
}
