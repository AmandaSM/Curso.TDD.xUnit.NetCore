using CursoOnline.Dominio.Cursos;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CursoOnline.Dominio.Teste.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        [Fact]
        public void DeveAdicionarCurso()
        {
            var cursoDTO = new CursoDTO
            {//OBJ transferencia
                Nome = "Curso A",
                Descricao = "Descricao",
                CargaHoraria = 10.0,
                PublicoAlvo = 1,
                Valor = 10.0
            };

            //DomainService
            var cursoRepositorioMock = new Mock<ICursoRepositorio>();//valida se algum comportamento está funcionado ou n(ao chamalo)
            var armazenadorCurso = new ArmazenadorDeCurso(cursoRepositorioMock.Object);// injentando atraves de um construtor
            armazenadorCurso.Armazenar(cursoDTO);
            cursoRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Curso>()));
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
            var curso = new Curso(cursoDTO.Nome, cursoDTO.Descricao, cursoDTO.CargaHoraria, PublicoAlvo.Empreendedor,
                cursoDTO.Valor);
            _cursoRepositorio.Adicionar(curso);
        }
    }

    public class CursoDTO
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public int PublicoAlvo { get; set; }
        public double Valor { get; set; }
    }



}


