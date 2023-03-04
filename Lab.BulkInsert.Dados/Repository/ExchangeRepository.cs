using BenchmarkDotNet.Attributes;
using Lab.BulkInsert.Dados.Helper;
using Lab.BulkInsert.Dados.Model;
using Npgsql;
using System.Data;

namespace Lab.BulkInsert.Dados.Repository
{
    [MemoryDiagnoser]
    public class ExchangeRepository
    {
        private string ConnectionString = "Host=localhost;Username=postgres;Password=postgres;Database=exchange";

        [Benchmark]
        public void InsertAdo(Exchange exchange)
        {
            try
            {
                using var connection = new NpgsqlConnection(ConnectionString);
                connection.Open();

                using var cmd = new NpgsqlCommand();
                cmd.Connection = connection;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT INTO exchange (id, data, tipooperacao, ativo, quantidade, preco, conta) VALUES (@1, @2, @3, @4, @5, @6, @7)";
                cmd.Parameters.Add(new NpgsqlParameter("@1", exchange.id));
                cmd.Parameters.Add(new NpgsqlParameter("@2", exchange.data));
                cmd.Parameters.Add(new NpgsqlParameter("@3", exchange.tipoOperacao));
                cmd.Parameters.Add(new NpgsqlParameter("@4", exchange.ativo));
                cmd.Parameters.Add(new NpgsqlParameter("@5", exchange.quantidade));
                cmd.Parameters.Add(new NpgsqlParameter("@6", exchange.preco));
                cmd.Parameters.Add(new NpgsqlParameter("@7", exchange.conta));

                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception($"Falha ao inserir tabela cliente", e);
            }
        }

        [Benchmark]
        public void InsertBulk(List<Exchange> exchange)
        {
            try
            {
                var _Connection = new NpgsqlConnection(ConnectionString);
                _Connection.Open();

                var dataTable = DataTableHelper.GetDataTableForBulkInsert(exchange);

                using var bulk = new BulkInsert(_Connection);
                bulk.DestinationTableName = "exchange";
                bulk.WriteData(dataTable);

            }
            catch (Exception e)
            {
                throw new Exception($"falhou.'", e);
            }
        }

        //public void InsertBulkPosgres(List<Exchange> exchanges)
        //{
        //    using var connection = new NpgsqlConnection(ConnectionString);
        //    connection.Open();

        //    var dataTableExchange = DataTableHelper.GetDataTableForBulkInsert(exchanges);

        //    IDataSourceExchange t;
        //    var id = nameof(t.id);
        //    var data = nameof(t.data);
        //    var conta = nameof(t.conta);
        //    var ativo = nameof(t.ativo);
        //    var tipooperacao = nameof(t.tipoOperacao);
        //    var quantidade = nameof(t.quantidade);
        //    var preco = nameof(t.preco);



        //    using (var writer = connection.BeginBinaryImport("COPY exchange (id, data, conta, ativo, tipooperacao, quantidade, preco) FROM STDIN (FORMAT BINARY)"))
        //    {
        //        foreach (DataRow row in dataTableExchange.Rows)
        //        {
        //            writer.StartRow();
        //            writer.Write(row[id], NpgsqlTypes.NpgsqlDbType.Integer);
        //            writer.Write(row[data], NpgsqlTypes.NpgsqlDbType.Timestamp);
        //            writer.Write(row[conta], NpgsqlTypes.NpgsqlDbType.Integer);
        //            writer.Write(row[ativo], NpgsqlTypes.NpgsqlDbType.Varchar);
        //            writer.Write(row[tipooperacao], NpgsqlTypes.NpgsqlDbType.Varchar);
        //            writer.Write(row[quantidade], NpgsqlTypes.NpgsqlDbType.Integer);
        //            writer.Write(row[preco], NpgsqlTypes.NpgsqlDbType.Numeric);

        //        }
        //        writer.Complete();
        //    }
        //    connection.Close();
        //}
    }
}
