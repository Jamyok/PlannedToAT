    using System;
    using MySql.Data.MySqlClient;

    namespace PlannedToAT
    {
        public class DatabaseTester
        {
            private string connectionString = "hostname=plannedtoatscus.mysql.database.azure.com,port=3306,username=Jamy,password=Faye2011,";

            public void TestConnection()
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        Console.WriteLine("Connection successful!");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Connection failed: " + ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
