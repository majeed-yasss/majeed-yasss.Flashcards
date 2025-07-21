using Dapper;
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
    public static void CreateStack(string name)
    {
        var connection = new SqlConnection(ConnectionString);
        string cmd = "insert into stacks vlaues(@Name)";

        connection.Open();
        connection.Execute(cmd, new { Name = name });
    }
    
    public static void RenameStack(int id, string name)
    {
        var connection = new SqlConnection(ConnectionString);
        string cmd = "update stacks set Name = @Name where Id = @Id";

        connection.Open();
        connection.Execute(cmd, new { Name = name });
    }
    public static void DeleteStack(int id) 
    {
        var connection = new SqlConnection(ConnectionString);
        string cmd = "delete from stacks where Id = @Id";

        connection.Open();
        connection.Execute(cmd, new { Id = id });
    }
}
