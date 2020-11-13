using CursoOnline.Dominio.Cursos;
using System;

namespace CursoOnline.Dominio.Teste.Cursos
{
    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public ArmazenadorDeCurso(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;
        }

        public void Armazenar(CursoDTO cursoDTO)
        {
            var cursoJaSalvo = _cursoRepositorio.ObterPeloNome(cursoDTO.Nome);//retorna null pois n configurou setup
            if (cursoJaSalvo != null)
            {
                throw new ArgumentException("Nome do curso já contem no banco de Dados");

            }
          ;//parseando(quest) e criando uma variavel
            if (!Enum.TryParse<PublicoAlvo>(cursoDTO.PublicoAlvo, out var publicoAlvo))
            {
                throw new ArgumentException("Publico alvo invalido");
            }

            var curso = new Curso(cursoDTO.Nome, cursoDTO.Descricao, cursoDTO.CargaHoraria, publicoAlvo,
                cursoDTO.Valor);
            _cursoRepositorio.Adicionar(curso);
        }
    }
}
