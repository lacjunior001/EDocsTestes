using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDocsTestes.Servicos
{
    /// <summary>
    /// Métodos estáticos e utilidade geral.
    /// </summary>
    internal static class Uteis
    {
        internal static string EncodeBase64(string valor)
        {
            var valueBytes = Encoding.UTF8.GetBytes(valor);
            return Convert.ToBase64String(valueBytes);
        }
    }
}
