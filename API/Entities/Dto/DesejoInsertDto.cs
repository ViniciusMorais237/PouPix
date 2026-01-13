namespace API.Entities.Dto
{
    public class DesejoInsertDto
    {
        public string Nome { get; set; } = string.Empty;
        public double Valor { get; set; }
        public IFormFile? Imagem { get; set; }
    }
}