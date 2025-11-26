using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities.Exception
{
    public class ApiException(string codigoStatus, string mensagem, string? detalhes = null)
    {
        public string CodigoStatus { get; set; } = codigoStatus;
        public string Mensagem { get; set; } = mensagem;
        public string? Detalhes { get; set; } = detalhes;
    }
}