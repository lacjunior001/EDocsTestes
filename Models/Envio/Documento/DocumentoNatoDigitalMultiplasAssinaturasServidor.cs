using Hyland.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDocsTestes.Models.Envio.Documento
{
    internal class DocumentoNatoDigitalMultiplasAssinaturasServidor : DocumentoBase
    {
        internal Guid IdPapelCapturador { get; set; }
        internal Guid IdClasse { get; set; }        
        internal List<Guid> Assinantes { get; set; }

        public DocumentoNatoDigitalMultiplasAssinaturasServidor(Document documento, Guid papel) : base(documento, papel)
        {
            IdPapelCapturador = papel;
            Assinantes = new List<Guid>();
        }
    }
}
