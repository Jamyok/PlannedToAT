    using System;
    using MySql.Data.MySqlClient;

    namespace PlannedToAT
    {
        public class DatabaseTester
        {
            private string connectionString = "Server=your-server-address;Database=plannedtoat;User ID=your-username;Password=your-password;";

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
