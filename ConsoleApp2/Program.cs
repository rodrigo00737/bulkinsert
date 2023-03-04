using System.Text.Json;
using BenchmarkDotNet.Running;
using Lab.BulkInsert.Dados.Model;
using Lab.BulkInsert.Dados.Service;

string commandsJson = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "operacoes.json"));

var operacoes = JsonSerializer.Deserialize<List<Exchange>>(commandsJson) ?? new List<Exchange>();
var service = new ExchangeService();

var watch = new System.Diagnostics.Stopwatch();
watch.Start();

//service.Inserir(operacoes);

ExchangeService.InserirBulk(operacoes);
watch.Stop();

var summary = BenchmarkRunner.Run<ExchangeService>();

Console.WriteLine($"Tempo execução : {watch.Elapsed.TotalSeconds} ");
Console.Read();
