using CursoOnline.Dominio.Cursos;
using System;

namespace CursoOnline.Dominio.Teste.Cursos
{
    public class Curso
    {
        public string Nome { get; private set; }
        public string Descricao { get; private set; }
        public double CargaHoraria { get; private set; }
        public PublicoAlvo PublicoAlvo { get; private set; }
        public double Valor { get; private set; }

        public Curso(string nome, string descricao, double cargaHoraria, PublicoAlvo publicoAlvo, double valor)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new ArgumentException("Nome invalido");
            }
            if (string.IsNullOrEmpty(descricao))
            {
                throw new ArgumentException("Descrição invalida");
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
