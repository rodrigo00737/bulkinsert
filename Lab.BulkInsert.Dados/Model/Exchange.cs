namespace Lab.BulkInsert.Dados.Model
{
    public class Exchange
    {
        public int id { get; set; }
        public DateTime data { get; set; }
        public string tipoOperacao { get; set; }
        public string ativo { get; set; }
        public int quantidade { get; set; }
        public double preco { get; set; }
        public int conta { get; set; }
    }
}
