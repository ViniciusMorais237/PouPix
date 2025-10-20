using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class Anotacao
    {
        public int Id { get; set; }
        public string Texto { get; set; } = string.Empty;
        public DateTime Data { get; set; } = DateTime.UtcNow;
        public string? ImagemUrl { get; set; } = string.Empty;
    }
}