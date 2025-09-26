namespace API.Entities
{
    public class IndicadoresPaginaInicial
    {
        public int Id { get; set; }
        public decimal Saldo { get; set; }
        public decimal Renda { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal LimiteCreditoTotal { get; set; }
        public DateTime DiaPagamento { get; set; }
        
    }
}