using EDocsBibliotecaLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDocsTestes.Models.Envio.Documento
{
    /// <summary>
    /// Objeto base dos endpoints utilizados na 3º etapa do envio de documento.
    /// </summary>
    public abstract class DocumentoBase
    {
        /// <summary>
        /// Nome que o arquivo vai receber no E-Docs.
        /// </summary>
        public string NomeArquivo { get; private set; }

        /// <summary>
        /// Se quem está enviando terá acesso ao documento depois do envio. Padrão = true.
        /// </summary>
        public bool CredenciarCapturador { get; private set; }

        /// <summary>
        /// Regras sobre a visibilidade do documento no E-Docs.
        /// </summary>
        internal RestricaoAcesso RestricaoAcesso { get; set; }

        /// <summary>
        /// Identificador do documento gerado na fase 1.
        /// </summary>
        internal string? IdentificadorTemporarioArquivoNaNuvem { get; set; }

        protected DocumentoBase(Hyland.Unity.Document documento, Guid papel)
        {
            if (!"E-Docs Termo de upload de documento".Equals(documento.DocumentType.Name))
            {
                throw new Exception("DocumentoBase.Este não é o tipo documental corrento(OnBase).");
            }

            CredenciarCapturador = true;

            bool nomeEncontrado = false;
            bool credenciarEncontrado = false;

            for (int i = 0; i < documento.KeywordRecords.Count; i++)
            {
                foreach (var item in documento.KeywordRecords[i].Keywords)
                {
                    switch (item.KeywordType.Name)
                    {
                        case "EDocs Doc: Credenciar Capturador":
                            if ("TRUE".Equals(item.AlphaNumericValue))
                            {
                                CredenciarCapturador = true;
                            }
                            else if ("FALSE".Equals(item.AlphaNumericValue))
                            {
                                CredenciarCapturador = false;
                            }
                            credenciarEncontrado = true;

                            break;

                        case "EDocs Doc: Nome Arquivo":
                            if (item.IsBlank || string.IsNullOrWhiteSpace(item.AlphaNumericValue))
                            {
                                throw new Exception("DocumentoBase.Nome Arquivo é campo obrigatório.");
                            }
                            else
                            {
                                NomeArquivo = item.AlphaNumericValue;
                            }
                            nomeEncontrado = true;

                            break;
                    }

                    if (nomeEncontrado && credenciarEncontrado)
                    {
                        goto Fim;
                    }
                }
            }
        Fim:

            RestricaoAcesso = new RestricaoAcesso(documento, papel);
        }
    }
}
