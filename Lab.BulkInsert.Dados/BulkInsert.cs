using Npgsql;
using NpgsqlTypes;
using System.Data;

namespace Lab.BulkInsert.Dados
{
    public class BulkInsert : IDisposable
    {
        private NpgsqlConnection _connection;
        private NpgsqlTransaction? _transaction;        
        private string _destinationTableName;

        public BulkInsert(NpgsqlConnection connection) : this(connection, null)
        {
        }

        public BulkInsert(NpgsqlConnection connection, NpgsqlTransaction? transation = null)
        {
            _connection = connection;
            _transaction = transation;
        }

        public string DestinationTableName
        {
            get => _destinationTableName;
            set
            {
                if (string.IsNullOrEmpty(value))
                    throw new ArgumentException("Destination Table Name cannot be null or empty string");
                _destinationTableName = value;
            }
        }         

        public void WriteData(DataTable table)
        {
            if (table == null)
                throw new ArgumentNullException(nameof(table));

            var commandText = BuildCopyCommandText(table);

            WriteInsertBulkData(table, commandText);
        }        

        private string BuildCopyCommandText(DataTable table)
        {
            var commandText = $"COPY {DestinationTableName} (@@columnList@@) FROM STDIN (FORMAT BINARY)";            
            var columnList = GetColumnList(table);            

            commandText = commandText.Replace("@@columnList@@", columnList);            

            return commandText;
        }

        private void WriteInsertBulkData(DataTable table, string copyCommandText)
        {
            ValidateConnection();            

            using var writer = _connection.BeginBinaryImport(copyCommandText);
            var resultado = GetParametersWrite(table);

            foreach (DataRow row in table.Rows)
            {
                writer.StartRow();
                writer.Write(row[resultado.Keys.ToList()[0]], resultado.Values.ToList()[0]);
                writer.Write(row[resultado.Keys.ToList()[1]], resultado.Values.ToList()[1]);
                writer.Write(row[resultado.Keys.ToList()[2]], resultado.Values.ToList()[2]);
                writer.Write(row[resultado.Keys.ToList()[3]], resultado.Values.ToList()[3]);
                writer.Write(row[resultado.Keys.ToList()[4]], resultado.Values.ToList()[4]);
                writer.Write(row[resultado.Keys.ToList()[5]], resultado.Values.ToList()[5]);
                writer.Write(row[resultado.Keys.ToList()[6]], resultado.Values.ToList()[6]);

            }
            writer.Complete();
        }

        private static Dictionary<string, NpgsqlDbType> GetParametersWrite(DataTable data)
        {
            var parameters = new Dictionary<string, NpgsqlDbType>();
            foreach (DataColumn c in data.Columns)
            {
                var dbType = GetNgpsqDbTypeFromDotnetType(c.DataType);
                var columnData = data.AsEnumerable().Select(r => r.Field<object>(c.ColumnName));

                parameters.Add(c.ColumnName, dbType);
            }

            return parameters;
        }

        private void ValidateConnection()
        {
            if (_connection == null)
                throw new Exception("É necessário o Postgres Database Connection");
        }

        private void OpenConnection()
        {
            if (_connection.State != ConnectionState.Open)
                _connection.Open();
        }

        private static string GetColumnList(DataTable data)
        {
            var columnNames = data.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            var columnList = string.Join(",", columnNames);
            return columnList;
        }     

        private static NpgsqlDbType GetNgpsqDbTypeFromDotnetType(Type t)
        {
            if (t == typeof(string)) return NpgsqlDbType.Varchar;
            if (t == typeof(DateTime)) return NpgsqlDbType.Timestamp;
            if (t == typeof(decimal)) return NpgsqlDbType.Numeric;
            if (t == typeof(int)) return NpgsqlDbType.Integer;

            if (t == typeof(long)) return NpgsqlDbType.Integer;
            if (t == typeof(short)) return NpgsqlDbType.Integer;
            if (t == typeof(sbyte)) return NpgsqlDbType.Bytea;

            if (t == typeof(float)) return NpgsqlDbType.Numeric;
            if (t == typeof(double)) return NpgsqlDbType.Numeric;

            return NpgsqlDbType.Varchar;
        }

        void IDisposable.Dispose()
        {
            _connection.Close();
            _connection.Dispose();
            _connection = null;
        }
    }
}