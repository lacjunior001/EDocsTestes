using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDocsTestes.Models.Recebimento
{
    internal class Documento
    {
        internal Guid Id { get; set; }
        internal string Registro { get; set; }
        internal string Nome { get; set; }
        internal string Extensao { get; set; }
        internal NivelAcesso NivelAcesso { get; set; }
    }
}
