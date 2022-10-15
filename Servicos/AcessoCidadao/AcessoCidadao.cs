using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDocsTestes.Servicos.AcessoCidadao
{
    internal abstract class AcessoCidadao
    {
        protected string JwtToken { get; set; }
        protected DateTime Validade { get; set; }
        protected static Configuracoes Configuracoes { get; set; }

        internal abstract string RecuperarToken();
    }
}
