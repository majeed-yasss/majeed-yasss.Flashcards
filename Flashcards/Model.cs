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
    public static string ConnectionString 
    {
        get 
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            return config.GetConnectionString("constr");
        }
    }
    public static void LoadDatabase()
    {
        var connection = new SqlConnection(ConnectionString);
        connection.Open();
    }
    public static void CreateStack(string Name)
    {
        var connection = new SqlConnection(ConnectionString);
        string cmd = "insert into stacks(Name) values (@Name)";

        connection.Open();
        connection.Execute(cmd, new { Name });
    }
    public static IEnumerable<T> RetriveRecords<T>() where T : ITable
    {
        var connection = new SqlConnection(ConnectionString);
        string cmd = $"Select * From {T.TableName}";

        connection.Open();
        IEnumerable<T> records = connection.Query<T>(cmd);
        return records;
    }
    public static void RenameStack(int Id, string Name)
    {
        var connection = new SqlConnection(ConnectionString);
        string cmd = "update stacks set Name = @Name where Id = @Id";

        connection.Open();
        connection.Execute(cmd, new { Id, Name });
    }
    public static void Delete(ITable row) 
    {
        var connection = new SqlConnection(ConnectionString);
        string cmd = "delete from stacks where Id = @Id";

        connection.Open();
        connection.Execute(cmd, new { row.Id });
    }
}
