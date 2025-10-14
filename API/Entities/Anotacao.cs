using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace API.Entities
{
    public class Anotacao
    {
        public string Texto { get; set; } = string.Empty;
        public DateTime Data { get; set; } = DateTime.Now;
        public IFormFile? Imagem { get; set; }
    }
}