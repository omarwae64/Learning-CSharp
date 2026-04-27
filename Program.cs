using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text.Json;
using AutoMapper;
using Dapper;
using Helloworld.Data;
using Helloworld.models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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
            // DataContextEF entityFramework = new DataContextEF(config);



            // string connectionString = "Server=127.0.0.1,14335;Database=dotnetcoursedatabase;User Id=sa;Password=SuperStrong@Passw0rd123;TrustServerCertificate=True;";

            // IDbConnection dbConnection= new SqlConnection(connectionString);

            // string sqlCommand = "SELECT GETDATE()";

            // DateTime rightNow = dapper.LoadDataSingle<DateTime>(sqlCommand);

            // Console.WriteLine(rightNow);


            // Computer myComputer = new Computer()
            // {
            //     Motherboard = "Z690",
            //     HasWifi = true,
            //     HasLTE = false,
            //     ReleaseDate = DateTime.Now,
            //     Price = 864.45m,
            //     VideoCard = "RTX 8056"
            // };


            // entityFramework.Add(myComputer);
            // entityFramework.SaveChanges();


    
            // string sql =  @"INSERT INTO TutorialAppSchema.Computer ( 
            //     Motherboard,
            //     HasWifi,
            //     HasLTE,
            //     ReleaseDate,
            //     Price,
            //     VideoCard
            
            // ) VALUES ('" + myComputer.Motherboard 
            //         +"','"+ myComputer.HasWifi
            //         +"','"+ myComputer.HasLTE
            //         +"','"+ myComputer.ReleaseDate
            //         +"','"+ myComputer.Price
            //         +"','"+ myComputer.VideoCard

            // +  "')";


            // File.WriteAllText("log.txt", "\n" + sql + "\n");

            // using StreamWriter openFile = new("log.txt", append: true);

            // openFile.WriteLine("\n" + sql + "\n");

            // openFile.Close();

            string Computerjson = File.ReadAllText("ComputersSnake.json");


        

            Mapper mapper = new Mapper(new MapperConfiguration((cfg) =>
            {

                cfg.CreateMap<ComputerSnake, Computer>()
                    .ForMember(destination => destination.ComputerId, options=> 
                        options.MapFrom(source => source.computer_id))
                        .ForMember(destination => destination.CPUCores, options=> 
                        options.MapFrom(source => source.cpu_cores))
                        .ForMember(destination => destination.HasLTE, options=> 
                        options.MapFrom(source => source.has_lte))
                        .ForMember(destination => destination.HasWifi, options=> 
                        options.MapFrom(source => source.has_wifi))
                        .ForMember(destination => destination.Motherboard, options=> 
                        options.MapFrom(source => source.motherboard))
                        .ForMember(destination => destination.ReleaseDate, options=> 
                        options.MapFrom(source => source.release_date))
                        .ForMember(destination => destination.Price, options=> 
                        options.MapFrom(source => source.price));
                        
            }));

            IEnumerable<ComputerSnake>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(Computerjson);

            if (computersSystem != null)
            {

                IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersSystem);
                Console.WriteLine("Automapper Count: " + computerResult.Count());

                foreach (ComputerSnake computer in computersSystem)
                {
                    Console.WriteLine(computer.motherboard);
                }
            }

            IEnumerable<Computer>? computersJsonPropertyMapping = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(Computerjson);

            if (computersJsonPropertyMapping != null)
            {

                Console.WriteLine("Json Property Count:"+computersJsonPropertyMapping.Count());


                // foreach (Computer computer in computersJsonPropertyMapping)
                // {
                //     Console.WriteLine(computer.Motherboard);
                // }
            }


            // System.Console.WriteLine(Computerjson);

            // JsonSerializerOptions options = new JsonSerializerOptions()
            // {
            //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            // };

            // IEnumerable<Computer>? computers = JsonSerializer.Deserialize<IEnumerable<Computer>>(Computerjson, options);
            // IEnumerable<Computer>? computers = JsonConvert.DeserializeObject<IEnumerable<Computer>>(Computerjson);


            // if (computers != null)
            // {
            //     foreach( Computer computer in computers)
            //     {
            //         Console.WriteLine(computer.Motherboard);
            //     }
            // }

        }
    
      }
}



