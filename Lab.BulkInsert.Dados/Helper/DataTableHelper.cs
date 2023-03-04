using Lab.BulkInsert.Dados.Model;
using System.Data;

namespace Lab.BulkInsert.Dados.Helper
{
    public class DataTableHelper
    {
        public static DataTable GetDataTableForBulkInsert(IEnumerable<Exchange> exchanges)
        {
            IDataSourceExchange t;
            var dataTable = new DataTable();

            var id = nameof(t.id);
            var data = nameof(t.data);
            var conta = nameof(t.conta);
            var ativo = nameof(t.ativo);
            var tipooperacao = nameof(t.tipoOperacao);
            var quantidade = nameof(t.quantidade);
            var preco = nameof(t.preco); 

            dataTable.Columns.Add(new DataColumn("id", typeof(int)));
            dataTable.Columns.Add(new DataColumn("data", typeof(DateTime)));
            dataTable.Columns.Add(new DataColumn("conta", typeof(int)));
            dataTable.Columns.Add(new DataColumn("ativo", typeof(string)));
            dataTable.Columns.Add(new DataColumn("tipooperacao", typeof(string)));
            dataTable.Columns.Add(new DataColumn("quantidade", typeof(int)));
            dataTable.Columns.Add(new DataColumn("preco", typeof(double)));            

            foreach (var exchange in exchanges)
            {
                var dataRow = dataTable.NewRow();
                dataRow[id] = exchange.id;
                dataRow[data] = exchange.data;
                dataRow[conta] = exchange.conta;
                dataRow[ativo] = exchange.ativo;
                dataRow[tipooperacao] = exchange.tipoOperacao;
                dataRow[quantidade] = exchange.quantidade;
                dataRow[preco] = exchange.preco;                

                dataTable.Rows.Add(dataRow);
            }                       

            return dataTable ?? new();
        }
    }
}
