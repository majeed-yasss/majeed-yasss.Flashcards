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
            " SELECT * FROM stacks WHERE Name = @Name";

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
    public IEnumerable<T> RetriveRecords<T>(int StackId) where T : ITable
    {
        var connection = new SqlConnection(_connectionString);
        string cmd = $"Select * From {T.TableName} "
        + $"WHERE StackId = @StackID";

        connection.Open();
        IEnumerable<T> records = connection.Query<T>(cmd, new { StackId });
        return records;
    }
    public void RenameStack(int Id, string Name)
    {
        var connection = new SqlConnection(_connectionString);
        string cmd = "update stacks set Name = @Name where Id = @Id";

        connection.Open();
        connection.Execute(cmd, new { Id, Name });
    }
    public bool IsExistStack(string Name)
    {
        var connection = new SqlConnection(_connectionString);
        string cmd ="SELECT 1 FROM stacks WHERE Name = @Name";

        connection.Open();
        int? res = connection.QuerySingleOrDefault<int>(cmd, new { Name });
        return res is not null && res > 0;
    }
    public void Delete<T>(T row) where T : ITable => Delete<T>(row.Id);
    public void Delete<T>(int Id) where T : ITable
    {
        var connection = new SqlConnection(_connectionString);
        string cmd = $"delete from {T.TableName} where Id = @Id";

        connection.Open();
        connection.Execute(cmd, new { Id });
    }

    internal void CreateFlashcard(Flashcard flashcard)
    {
        var connection = new SqlConnection(_connectionString);
        string cmd =
            $"insert into {Flashcard.TableName}(StackId, Front, Back) " +
            $"values (@StackId, @Front, @Back)";

        var param = new
        {
            flashcard.StackId,
            flashcard.Front,
            flashcard.Back
        };

        connection.Open();
        connection.Execute(cmd, param);
    }
    internal void EditFlashcard(int id, Flashcard flashcard)
    {
        var connection = new SqlConnection(_connectionString);
        string cmd =
            $"UPDATE {Flashcard.TableName}" +
            $" SET Front = @Front, Back = @Back" +
            $" WHERE Id = @Id";

        var param = new
        {
            Id = id,
            flashcard.Front,
            flashcard.Back
        };

        connection.Open();
        connection.Query<Flashcard>(cmd, param);
    }
}
