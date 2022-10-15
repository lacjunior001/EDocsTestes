using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDocsTestes.Models.Recebimento
{
    internal enum NivelAcesso : short
    {
        Publico = 1,
        Organizacional = 2,
        Restrito = 3,
        Sigiloso = 4,
        Reservado = 5,
        Secreto = 6,
        Ultrassecreto = 7,
        SigilosoSemFundamentoLegal = 8
    }
}
