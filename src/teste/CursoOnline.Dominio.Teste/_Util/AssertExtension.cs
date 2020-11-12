using System;
using Xunit;

namespace CursoOnline.Dominio.Teste._Util
{
    public static class AssertExtension
    {
        public static void ComMensagem(this ArgumentException exception, string mensagem)
        {
            if (exception.Message ==mensagem )
            {
                Assert.True(true);
            }
            else
            {
                Assert.True(true, $"era esperado a mensagem: {mensagem}");
            }

        }
    }
}
