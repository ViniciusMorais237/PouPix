using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TestesController : BaseController
    {

        [HttpPost]
        public async Task<IActionResult> ObterUsuarioLogado(UsuarioDto usuario)
        {
            return Ok(new Usuario(usuario.Matricula, usuario.Nome, usuario.Saldo));
        }

        public class UsuarioDto()
        {
            public string Matricula { get; set; } = string.Empty;
            public string Nome { get; set; } = string.Empty;
            public decimal Saldo { get; set; }
        }


        public class Usuario(string matricula, string nome, decimal saldo)
        {
            public Matricula Matricula { get; private set; } = new(matricula);
            public string Nome { get; private set; } = nome;
            public decimal Saldo { get; private set; } = saldo;
        }

        public class Matricula
        {
            public Matricula(string matricula)
            {
                Texto = matricula;
                Codigo = TransformarMatriculaEmCodigo(matricula);
            }
            public string Texto { get; set; } 
            public int Codigo { get; set; }

            public int TransformarMatriculaEmCodigo(string matricula)
            {
                var caracter = matricula[0];
                return int.TryParse(matricula.AsSpan(1), out int codigo) && Char.IsLetter(caracter) && matricula.Length < 8 ? codigo : throw new InvalidDataException("matricula fora do padrão X123456");
            }
        }
    }
}