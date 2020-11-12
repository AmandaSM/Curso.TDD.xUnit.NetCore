using CursoOnline.Dominio.Teste._Util;
using ExpectedObjects;
using Microsoft.VisualStudio.TestPlatform.Common.Telemetry;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CursoOnline.Dominio.Teste.Cursos
{
    public class CursoTest
    {
        [Fact]
        public void DeveCriarCurso()
        {
            //ORGANIZAÇÃO
            var cursoEsperado = new
            {
                Nome = "informatica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
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
            //ORGANIZAÇÃO
            var cursoEsperado = new
            {
                Nome = "informatica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            //ASSERT
            Assert.Throws<ArgumentException>(() =>
                  new Curso(nomeInvalido, cursoEsperado.CargaHoraria, cursoEsperado.PublicoAlvo, cursoEsperado.Valor))
                  .ComMensagem("Nome invalido");

        }


        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void NaoDeveTerUmaCargaHorariaMenorQue1(double cargaHorariaInvalida)
        {
            //ORGANIZAÇÃO
            var cursoEsperado = new
            {
                Nome = "informatica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            //ASSERT
            Assert.Throws<ArgumentException>(() =>
            new Curso(cursoEsperado.Nome, cargaHorariaInvalida, cursoEsperado.PublicoAlvo, cursoEsperado.Valor))
                .ComMensagem("Carga horaria invalida");

        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        public void NaoDeveTerValorMenorQue1(double valorInvalido)
        {
            //ORGANIZAÇÃO
            var cursoEsperado = new
            {
                Nome = "informatica",
                CargaHoraria = (double)80,
                PublicoAlvo = PublicoAlvo.Estudante,
                Valor = (double)950
            };

            //ASSERT
            Assert.Throws<ArgumentException>(() =>
           new Curso(cursoEsperado.Nome, valorInvalido, cursoEsperado.PublicoAlvo, cursoEsperado.Valor))
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
