using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    class Update
    {
        static private String connectString = @"Server = .;" + "Database = BicycleStores; " + "Integrated Security=true;";

        public Int32 FieldChoice { get; set; }
        public Int32 SearchBy { get; set; }
        

        public void GetSearchChoice()
        {
            Console.WriteLine("Search by: ");
            Console.WriteLine("1) CustomerId");
            Console.WriteLine("2) Customer Name");
            var choice = Convert.ToInt32(Console.ReadLine());
            while (choice < 1 || choice > 2)
            {
                Console.WriteLine("Invalid choice.");
                choice = Convert.ToInt32(Console.ReadLine());
            }
            SearchBy = choice;
        }

        public void GetUpdateFieldChoice()
        {
            FieldChoice = 0;
            Console.WriteLine("Choose Field to Update:");
            Console.WriteLine("1) FirstName");
            Console.WriteLine("2) LastName");
            Console.WriteLine("3) Phone");
            Console.WriteLine("4) Email");
            Console.WriteLine("5) Street");
            Console.WriteLine("6) City");
            Console.WriteLine("7) State");
            Console.WriteLine("8) ZipCode");
            var fieldChoice = Convert.ToInt32(Console.ReadLine());
            while (fieldChoice < 1 || fieldChoice > 8)
            {
                Console.WriteLine("Invalid choice.");
                fieldChoice = Convert.ToInt32(Console.ReadLine());
            }

            FieldChoice = fieldChoice;
        }

        public void UpdateFieldById(Int32 customerId)
        {
            switch (FieldChoice)
            {
                case 1: // FirstName
                    UpdateFirstNameById(customerId);
                    break;
                case 2: // LastName
                    UpdateLastNameById(customerId);
                    break;
                case 3: // Phone
                    UpdatePhoneById(customerId);
                    break;
                case 4: // Email
                    UpdateEmailById(customerId);
                    break;
                case 5: // Street
                    UpdateStreetById(customerId);
                    break;
                case 6: // City
                    UpdateCityById(customerId);
                    break;
                case 7: // State
                    UpdateStateById(customerId);
                    break;
                case 8: // Zip
                    UpdateZipCodeById(customerId);
                    break;
            }
        }

        public void UpdateFieldByName(String firstName, String lastName)
        {
            switch (FieldChoice)
            {
                case 1: // FirstName
                    UpdateFirstNameByName(firstName, lastName);
                    break;
                case 2: // LastName
                    UpdateLastNameByName(firstName, lastName);
                    break;
                case 3: // Phone
                    UpdatePhoneByName(firstName, lastName);
                    break;
                case 4: // Email
                    UpdateEmailByName(firstName, lastName);
                    break;
                case 5: // Street
                    UpdateStreetByName(firstName, lastName);
                    break;
                case 6: // City
                    UpdateCityByName(firstName, lastName);
                    break;
                case 7: // State
                    UpdateStateByName(firstName, lastName);
                    break;
                case 8: // Zip
                    UpdateZipCodeByName(firstName, lastName);
                    break;
            }
        }

        public bool CheckRecordById(Int32 customerId)
        {
            var queryString = "SELECT * FROM Sales.Customers" +
                       " WHERE CustomerId = @inputCustomerId";

            using (var connection = new SqlConnection(connectString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@inputCustomerId", customerId);
                // execute query
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("\n\n");
                        Console.WriteLine("Customer not found.");
                        return false;
                    }
                    else
                    {
                        Console.WriteLine("\n\n");
                        while (reader.Read())
                        {
                            
                            Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                              $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                              $"Zip: {reader["ZipCode"]}");
                            Console.WriteLine();

                        }

                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
                return true;
            }
        }
        
        public bool CheckRecordByName(String firstname, String lastName)
        {
            var queryString = "SELECT * FROM Sales.Customers" +
                        " WHERE FirstName = @inputFirstName AND LastName = @inputLastName";

            using (var connection = new SqlConnection(connectString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@inputFirstName", firstname);
                command.Parameters.AddWithValue("@inputLastName", lastName);

                // execute query
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();
                   

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("\n\n");
                        Console.WriteLine("Customer not found.");
                        return false;
                    }
                    Console.WriteLine("\n\n");
                    Console.WriteLine("Customer Info:\n");
                    while (reader.Read())
                    {
                       
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                          $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                          $"Zip: {reader["ZipCode"]}");
                        Console.WriteLine();
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
                return true;
            }
        }

        public void UpdateFirstNameById(Int32 CustomerId)
        {
            Console.WriteLine("Enter new FirstName: ");
            var inputFirstName = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET FirstName = @inputFirstName" +
                " WHERE CustomerId = @inputCustomerId";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE CustomerId = @inputCustomerId";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputFirstName", inputFirstName);
                updateCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                queryCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }
                    
                    Console.WriteLine();
                }
                catch(SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        public void UpdateLastNameById(Int32 CustomerId)
        {
            Console.WriteLine("Enter new LastName: ");
            var inputLastName = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET LastName = @inputLastName" +
                " WHERE CustomerId = @inputCustomerId";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE CustomerId = @inputCustomerId";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputLastName", inputLastName);
                updateCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                queryCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        public void UpdatePhoneById(Int32 CustomerId)
        {
            Console.WriteLine("Enter new Phone: ");
            var inputPhone = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET Phone = @inputPhone" +
                " WHERE CustomerId = @inputCustomerId";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE CustomerId = @inputCustomerId";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputPhone", inputPhone);
                updateCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                queryCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        public void UpdateEmailById(Int32 CustomerId)
        {
            Console.WriteLine("Enter new Email: ");
            var inputEmail = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET Email = @inputEmail" +
                " WHERE CustomerId = @inputCustomerId";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE CustomerId = @inputCustomerId";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputEmail", inputEmail);
                updateCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                queryCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        public void UpdateStreetById(Int32 CustomerId)
        {
            Console.WriteLine("Enter new Street: ");
            var inputStreet = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET Street = @inputStreet" +
                " WHERE CustomerId = @inputCustomerId";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE CustomerId = @inputCustomerId";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputStreet", inputStreet);
                updateCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                queryCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        public void UpdateCityById(Int32 CustomerId)
        {
            Console.WriteLine("Enter new City: ");
            var inputCity = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET City = @inputCity" +
                " WHERE CustomerId = @inputCustomerId";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE CustomerId = @inputCustomerId";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputCity", inputCity);
                updateCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                queryCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        public void UpdateStateById(Int32 CustomerId)
        {
            Console.WriteLine("Enter new State: ");
            var inputState = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET State = @inputState" +
                " WHERE CustomerId = @inputCustomerId";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE CustomerId = @inputCustomerId";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputState", inputState);
                updateCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                queryCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        public void UpdateZipCodeById(Int32 CustomerId)
        {
            Console.WriteLine("Enter new ZipCode: ");
            var inputZipCode = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET ZipCode = @inputZipCode" +
                " WHERE CustomerId = @inputCustomerId";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE CustomerId = @inputCustomerId";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputZipCode", inputZipCode);
                updateCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                queryCommand.Parameters.AddWithValue("@inputCustomerId", CustomerId);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        // ------------------- By Name -----------------------------------------//
        
        public void UpdateFirstNameByName(String firstName, String lastName)
        {
            Console.WriteLine("Enter new FirstName: ");
            var inputFirstName = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET FirstName = @inputFirstName" +
                " WHERE FirstName = @oldFirstName AND LastName = @oldLastName";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE FirstName = @inputFirstName AND LastName = @oldLastName";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputFirstName", inputFirstName);
                updateCommand.Parameters.AddWithValue("@oldFirstName", firstName);
                updateCommand.Parameters.AddWithValue("@oldLastName", lastName);

                queryCommand.Parameters.AddWithValue("@inputFirstName", inputFirstName);
                queryCommand.Parameters.AddWithValue("@oldLastName", lastName);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        public void UpdateLastNameByName(String firstName, String lastName)
        {
            Console.WriteLine("Enter new LastName: ");
            var inputLastName = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET LastName = @inputLastName" +
                " WHERE FirstName = @oldFirstName AND LastName = @oldLastName";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE FirstName = @oldFirstName AND LastName = @inputLastName";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputLastName", inputLastName);
                updateCommand.Parameters.AddWithValue("@oldFirstName", firstName);
                updateCommand.Parameters.AddWithValue("@oldLastName", lastName);

                queryCommand.Parameters.AddWithValue("@inputLastName", inputLastName);
                queryCommand.Parameters.AddWithValue("@oldFirstName", firstName);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        public void UpdatePhoneByName(String firstName, String lastName)
        {
            Console.WriteLine("Enter new Phone: ");
            var inputPhone = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET Phone = @inputPhone" +
                " WHERE FirstName = @oldFirstName AND LastName = @oldLastName";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE FirstName = @oldFirstName AND LastName = @oldLastName";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputPhone", inputPhone);
                updateCommand.Parameters.AddWithValue("@oldFirstName", firstName);
                updateCommand.Parameters.AddWithValue("@oldLastName", lastName);

                queryCommand.Parameters.AddWithValue("@oldFirstName", firstName);
                queryCommand.Parameters.AddWithValue("@oldLastName", lastName);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        public void UpdateEmailByName(String firstName, String lastName)
        {
            Console.WriteLine("Enter new Email: ");
            var inputEmail = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET Email = @inputEmail" +
                " WHERE FirstName = @oldFirstName AND LastName = @oldLastName";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE FirstName = @oldFirstName AND LastName = @oldLastName";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputEmail", inputEmail);
                updateCommand.Parameters.AddWithValue("@oldFirstName", firstName);
                updateCommand.Parameters.AddWithValue("@oldLastName", lastName);

                queryCommand.Parameters.AddWithValue("@oldFirstName", firstName);
                queryCommand.Parameters.AddWithValue("@oldLastName", lastName);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        public void UpdateStreetByName(String firstName, String lastName)
        {
            Console.WriteLine("Enter new Street: ");
            var inputStreet = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET Street = @inputStreet" +
                " WHERE FirstName = @oldFirstName AND LastName = @oldLastName";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE FirstName = @oldFirstName AND LastName = @oldLastName";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputStreet", inputStreet);
                updateCommand.Parameters.AddWithValue("@oldFirstName", firstName);
                updateCommand.Parameters.AddWithValue("@oldLastName", lastName);

                queryCommand.Parameters.AddWithValue("@oldFirstName", firstName);
                queryCommand.Parameters.AddWithValue("@oldLastName", lastName);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        public void UpdateCityByName(String firstName, String lastName)
        {
            Console.WriteLine("Enter new City: ");
            var inputCity = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET City = @inputCity" +
                " WHERE FirstName = @oldFirstName AND LastName = @oldLastName";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE FirstName = @oldFirstName AND LastName = @oldLastName";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputCity", inputCity);
                updateCommand.Parameters.AddWithValue("@oldFirstName", firstName);
                updateCommand.Parameters.AddWithValue("@oldLastName", lastName);

                queryCommand.Parameters.AddWithValue("@oldFirstName", firstName);
                queryCommand.Parameters.AddWithValue("@oldLastName", lastName);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        public void UpdateStateByName(String firstName, String lastName)
        {
            Console.WriteLine("Enter new State: ");
            var inputState = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET State = @inputState" +
                " WHERE FirstName = @oldFirstName AND LastName = @oldLastName";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE FirstName = @oldFirstName AND LastName = @oldLastName";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputState", inputState);
                updateCommand.Parameters.AddWithValue("@oldFirstName", firstName);
                updateCommand.Parameters.AddWithValue("@oldLastName", lastName);

                queryCommand.Parameters.AddWithValue("@oldFirstName", firstName);
                queryCommand.Parameters.AddWithValue("@oldLastName", lastName);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }

        public void UpdateZipCodeByName(String firstName, String lastName)
        {
            Console.WriteLine("Enter new ZipCode: ");
            var inputZipCode = Console.ReadLine();

            var updateString = "UPDATE Sales.Customers" +
                " SET ZipCode = @inputZipCode" +
                " WHERE FirstName = @oldFirstName AND LastName = @oldLastName";

            var queryString = "SELECT * FROM Sales.Customers" +
                " WHERE FirstName = @oldFirstName AND LastName = @oldLastName";

            using (var connection = new SqlConnection(connectString))
            {
                var updateCommand = new SqlCommand(updateString, connection);
                var queryCommand = new SqlCommand(queryString, connection);

                updateCommand.Parameters.AddWithValue("@inputZipCode", inputZipCode);
                updateCommand.Parameters.AddWithValue("@oldFirstName", firstName);
                updateCommand.Parameters.AddWithValue("@oldLastName", lastName);

                queryCommand.Parameters.AddWithValue("@oldFirstName", firstName);
                queryCommand.Parameters.AddWithValue("@oldLastName", lastName);

                // execute query
                try
                {
                    connection.Open();
                    var rows = updateCommand.ExecuteNonQuery();
                    var reader = queryCommand.ExecuteReader();
                    Console.WriteLine("\n\n");
                    Console.WriteLine(rows + " rows affected.");
                    Console.WriteLine("\n\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"FirstName: {reader["FirstName"]}\nLastName Name: {reader["LastName"]}\nPhone: {reader["Phone"]}\n" +
                                         $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                         $"Zip: {reader["ZipCode"]}");
                    }

                    Console.WriteLine();
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }
        }
    }
}



