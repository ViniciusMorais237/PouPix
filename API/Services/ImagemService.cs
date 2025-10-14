using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class ImagemService
    {
        public static string CriarArquivoDeImagem(IFormFile file)
        {
            var filePath = Path.Combine("Storage", file.FileName);

            using Stream fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);
            return filePath;
        }
    }
}