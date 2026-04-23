using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using Dapper;
using Helloworld.Data;
using Helloworld.models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Helloworld
{

        internal class Program
        {
            
        static void Main(string[] args)
        {
            
            IConfiguration config = new ConfigurationBuilder()
                    .AddJsonFile("appsettings.json")
                    .Build();

            DataContextDapper dapper = new DataContextDapper(config);
            DataContextEF entityFramework = new DataContextEF(config);



            string connectionString = "Server=127.0.0.1,14335;Database=dotnetcoursedatabase;User Id=sa;Password=SuperStrong@Passw0rd123;TrustServerCertificate=True;";

            IDbConnection dbConnection= new SqlConnection(connectionString);

            string sqlCommand = "SELECT GETDATE()";

            DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);

            // Console.WriteLine(rightNow);


            Computer myComputer = new Computer()
            {
                Motherboard = "Z690",
                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 864.45m,
                VideoCard = "RTX 8056"
            };


            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();


    
            string sql =  @"INSERT INTO TutorialAppSchema.Computer ( 
                Motherboard,
                HasWifi,
                HasLTE,
                ReleaseDate,
                Price,
                VideoCard
            
            ) VALUES ('" + myComputer.Motherboard 
                    +"','"+ myComputer.HasWifi
                    +"','"+ myComputer.HasLTE
                    +"','"+ myComputer.ReleaseDate
                    +"','"+ myComputer.Price
                    +"','"+ myComputer.VideoCard

            +  "')";


            File.WriteAllText("log.txt", "\n" + sql + "\n");

            using StreamWriter openFile = new("log.txt", append: true);

            openFile.WriteLine("\n" + sql + "\n");

            openFile.Close();

            string fileText = File.ReadAllText("log.txt");

            System.Console.WriteLine(fileText);

        }
    
      }
}



