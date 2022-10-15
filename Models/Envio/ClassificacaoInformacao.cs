using Hyland.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDocsTestes.Models.Envio
{
    /// <summary>
    /// Informações sobre documentos classificados.
    /// Esta Classe ainda não foi testada....
    /// </summary>
    public class ClassificacaoInformacao
    {
        public ushort PrazoAnos { get; private set; }
        public ushort PrazoMeses { get; private set; }
        public ushort PrazoDias { get; private set; }
        public string Justificativa { get; private set; }
        public Guid? IdPapelAprovador { get; private set; }

        public ClassificacaoInformacao(Document documento, Guid? papel)
        {
            if (papel == null)
            {
                this.IdPapelAprovador = null;
            }
            else
            {
                this.IdPapelAprovador = papel;
            }

            this.PrazoAnos = 0;
            this.PrazoMeses = 0;
            this.PrazoDias = 0;

            foreach (var keywordRecord in documento.KeywordRecords)
            {
                foreach (var keyword in keywordRecord.Keywords)
                {
                    switch (keyword.KeywordType.Name)
                    {
                        case "EDocs RestAce\\Prazo: Anos":
                            this.PrazoAnos = (ushort)keyword.Numeric9Value;
                            break;
                        case "EDocs RestAce\\Prazo: Meses":
                            this.PrazoMeses = (ushort)keyword.Numeric9Value;
                            break;
                        case "EDocs RestAce\\Prazo: Dias":
                            this.PrazoDias = (ushort)keyword.Numeric9Value;
                            break;
                        case "EDocs RestAce: Justificativa":
                            this.Justificativa = keyword.AlphaNumericValue;
                            break;
                    }
                }
            }

            if ((this.PrazoAnos == 0 && this.PrazoMeses == 0 && this.PrazoDias == 0) || string.IsNullOrWhiteSpace(this.Justificativa))
            {
                throw new Exception("Os dados de classificação de restrição de acesso são inadequados.");
            }
        }
    }
}
