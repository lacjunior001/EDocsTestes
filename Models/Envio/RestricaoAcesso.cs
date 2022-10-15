using Hyland.Unity;

namespace EDocsTestes.Models.Envio
{
    /// <summary>
    /// Define as regras de acesso de um Documento/Ato/Termo/Encaminhamento no E-Docs.
    /// Padrão sempre será organizacional.
    /// Esta classe ainda não foi testada.
    /// </summary>
    public class RestricaoAcesso
    {

        public bool TransparenciaAtiva { get; private set; }
        public List<Guid>? IdsFundamentosLegais { get; private set; }
        public ClassificacaoInformacao? ClassificacaoInformacao { get; private set; }

        public RestricaoAcesso(Hyland.Unity.Document documento, Guid? papel)
        {
            //Documentação
            //https://github.com/prodest/e-docs-documentacao/blob/master/API/RestricaoAcesso.md

            if (documento == null)
            {
                throw new Exception("RestricaoAcesso.O documento é nulo (OnBase).");
            }

            string? restAcesso = "Organizacional";

            foreach (var keywordRecord in documento.KeywordRecords)
            {
                foreach (var keyword in keywordRecord.Keywords)
                {
                    if ("EDocs RestAce: Tipo".Equals(keyword.KeywordType.Name))
                    {
                        restAcesso = keyword.AlphaNumericValue;
                        if (string.IsNullOrWhiteSpace(restAcesso))
                        {
                            restAcesso = "Organizacional";
                        }
                        goto AchouTipo;
                    }
                }
            }

        AchouTipo:

            switch (restAcesso)
            {
                case "Organizacional":
                    //O documento é visualizável por qualquer pessoa lotada nos Órgãos por onde o documento transitou.
                    //Basta enviar o modelo com transparenciaAtiva como false, e nulo no restante.
                    //Esta Opção ainda não foi testada.
                    this.TransparenciaAtiva = false;
                    this.IdsFundamentosLegais = null;
                    this.ClassificacaoInformacao = null;
                    break;

                case "Público":
                    //O documento é visualizável por qualquer pessoa.
                    //Basta enviar o modelo com transparenciaAtiva como true, e nulo no restante.
                    //Esta Opção ainda não foi testada.
                    this.TransparenciaAtiva = true;
                    this.IdsFundamentosLegais = null;
                    this.ClassificacaoInformacao = null;
                    break;

                case "Sigiloso":
                    //O documento é visualizável somente por pessoas diretamente credenciadas a ele.
                    //Basta enviar o modelo com transparenciaAtiva como false, a lista de identificadores de fundamentos legais adequados e classificação de informação como nulo.
                    //Documentos ligados ao sigilo da pessoa.
                    //Esta opção ainda não foi testada.
                    this.TransparenciaAtiva = false;
                    this.IdsFundamentosLegais = RecuperarFundamentos(documento);
                    if (this.IdsFundamentosLegais == null || this.IdsFundamentosLegais.Count == 0)
                    {
                        throw new Exception("RestricaoAcesso. Documentos sigilosos precisam de uma fundamentação legal.");
                    }
                    this.ClassificacaoInformacao = null;

                    break;

                case "Classificado":
                    //O documento é visualizável somente por pessoas previamente credenciadas para visualizar documentos com essa classificação.
                    //Basta enviar o modelo com transparenciaAtiva como false, a lista de identificadores de fundamentos legais adequados e as informações de classificação de informação.
                    //No geral são documentos que podem por em risco a segurança do estado.
                    //Esta opção ainda não foi testada.
                    this.TransparenciaAtiva = false;
                    this.IdsFundamentosLegais = RecuperarFundamentos(documento);
                    if (this.IdsFundamentosLegais == null || this.IdsFundamentosLegais.Count == 0)
                    {
                        throw new Exception("RestricaoAcesso. Documentos classificados precisam de uma fundamentação legal.");
                    }
                    this.ClassificacaoInformacao = new ClassificacaoInformacao(documento, papel);

                    break;

                default:
                    throw new Exception($"A restrição({restAcesso}) ainda não foi implementada.");
                    break;
            }
        }

        /// <summary>
        /// Recupera os valores de fundamentos legais de uma Kw.
        /// Este método ainda não foi testado.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        private List<Guid> RecuperarFundamentos(Document documento)
        {
            var lista = new List<Guid>();

            foreach (var keywordRecord in documento.KeywordRecords)
            {
                if ("EDocs RestAce\\Fundamentos Legais".Equals(keywordRecord.KeywordRecordType.Name))
                {
                    foreach (var keyword in keywordRecord.Keywords)
                    {
                        if ("EDocs RestAce\\Fundamentos Legais: GUID".Equals(keyword.KeywordType.Name))
                        {
                            if (!string.IsNullOrWhiteSpace(keyword.AlphaNumericValue))
                            {
                                if (Guid.TryParse(keyword.AlphaNumericValue, out Guid novaGuid))
                                {
                                    if (!lista.Exists(p => p == novaGuid))
                                    {
                                        lista.Add(novaGuid);
                                    }
                                }
                                else
                                {
                                    throw new Exception($"RecuperarFundamentos.O Valor: {keyword.AlphaNumericValue} não é uma GUID válida.");
                                }
                            }
                        }
                    }
                }
            }

            return lista;
        }
    }
}
