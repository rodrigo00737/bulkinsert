using BenchmarkDotNet.Attributes;
using Lab.BulkInsert.Dados.Model;
using Lab.BulkInsert.Dados.Repository;

namespace Lab.BulkInsert.Dados.Service
{
    [MemoryDiagnoser]
    public class ExchangeService
    {
        public ExchangeService()
        {
            
        }

        [Benchmark]
        public static void Inserir(List<Exchange> list)
        {
            var repository = new ExchangeRepository();
            foreach (var exchange in list)
            {
                repository.InsertAdo(exchange);
            }
        }

        [Benchmark]
        public static void InserirBulk(List<Exchange> list)
        {
            var repository = new ExchangeRepository();
            repository.InsertBulk(list);
        }        
    }
}
