using Dapper;
using DataObjects;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flashcards;

internal class Model
{
    public readonly string _connectionString;
    public Model()
    {
        var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        _connectionString = config.GetConnectionString("constr")?? "";
    }
    public Stack CreateStack(string Name)
    {
        var connection = new SqlConnection(_connectionString);
        string cmd = "insert into stacks(Name) values (@Name)" +
            " SELECT * FROM Stacks WHERE Name = @Name";

        connection.Open();
        return connection.QuerySingle<Stack>(cmd, new { Name });
    }
    public IEnumerable<T> RetriveRecords<T>() where T : ITable
    {
        var connection = new SqlConnection(_connectionString);
        string cmd = $"Select * From {T.TableName}";

        connection.Open();
        IEnumerable<T> records = connection.Query<T>(cmd);
        return records;
    }
    public void RenameStack(int Id, string Name)
    {
        var connection = new SqlConnection(_connectionString);
        string cmd = "update stacks set Name = @Name where Id = @Id";

        connection.Open();
        connection.Execute(cmd, new { Id, Name });
    }
    public void Delete<T>(T row) where T : ITable => Delete<T>(row.Id);
    public void Delete<T>(int Id) where T : ITable
    {
        var connection = new SqlConnection(_connectionString);
        string cmd = $"delete from {T.TableName} where Id = @Id";

        connection.Open();
        connection.Execute(cmd, new { Id });
    }
}
