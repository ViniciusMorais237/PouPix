namespace API.Entities
{
    public class InfoBank
    {
        public int Id { get; set; }
        public decimal Saldo { get; set; }
        public decimal Renda { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal LimiteCreditoTotal { get; set; }
        public decimal Fatura { get; set; }
        public DateTime DiaPagamento { get; set; }
        
    }
}