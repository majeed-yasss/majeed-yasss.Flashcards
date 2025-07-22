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
    public void LoadDatabase()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();
    }
    public void CreateStack(string Name)
    {
        var connection = new SqlConnection(_connectionString);
        string cmd = "insert into stacks(Name) values (@Name)";

        connection.Open();
        connection.Execute(cmd, new { Name });
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
    public void Delete(ITable row) 
    {
        var connection = new SqlConnection(_connectionString);
        string cmd = "delete from stacks where Id = @Id";

        connection.Open();
        connection.Execute(cmd, new { row.Id });
    }
}
