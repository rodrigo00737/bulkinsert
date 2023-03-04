namespace Lab.BulkInsert.Dados.Helper
{
    public interface IDataSourceExchange
    {
        int id { get; set; }
        DateTime data { get; set; }
        string tipoOperacao { get; set; }
        string ativo { get; set; }
        int quantidade { get; set; }
        double preco { get; set; }
        int conta { get; set; }
    };
    
}
