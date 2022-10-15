using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDocsTestes.Servicos.AcessoCidadao
{
    internal class Configuracoes
    {
        public Configuracoes()
        {
            try
            {

            }
            catch (Exception e)
            {
                throw new Exception("AcessoCidadao.Configuracoes.", e);
            }
        }

        internal string Uri { get; set; }
        internal string ClientId { get; set; }
        internal string ClientSecret { get; set; }
        internal List<string> ApiScopes { get; set; }
        internal List<string> InfoScopes { get; set; }
    }
}
