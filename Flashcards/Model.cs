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
    public string ConnectionString 
    {
        get 
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            return config.GetConnectionString("constr");
        }
    }

    public void LoadDatabase()
    {
        var connection = new SqlConnection(ConnectionString);
        connection.Open();
    }
    
}
