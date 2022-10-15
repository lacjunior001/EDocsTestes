using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDocsTestes.Servicos.AcessoCidadao
{
    internal class ContaOnBase : AcessoCidadao
    {
        internal override string RecuperarToken()
        {
            try
            {
                if (DateTime.Now.CompareTo(this.Validade) > 0)
                {

                }
                else
                {

                }
            }
            catch (Exception e)
            {
                throw new Exception("AcessoCidadao.ContaOnBase.RecuperarToken.", e);
            }

            return JwtToken;
        }

        private void Conectar()
        {
            try
            {
                var cliente = new RestClient(Configuracoes.Uri);
                var request = new RestRequest();
                request.Method = Method.Post;
                request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
                request.AddHeader("Authorization", $"Basic {Uteis.EncodeBase64($"{Configuracoes.ClientId}:{Configuracoes.ClientSecret}")}");
                request.AddParameter("grant_type", "client_credentials");
                string scopes = "";

                foreach (var item in Configuracoes.ApiScopes)
                {
                    scopes += item + " ";
                }

                request.AddParameter("scope", scopes);

                var response = cliente.Execute(request);

                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    if (response.Content.Length > 250)
                    {
                        response.Content = response.Content.Remove(250, response.Content.Length - 1);
                    }

                    throw new Exception($"Conectar.Código<<{response.StatusCode}>>.<<{response.Content}>>");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"ConectaAC.", e);
            }
        }
    }
}
