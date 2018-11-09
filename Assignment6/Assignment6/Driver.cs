using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6
{
    class Driver
    {
        static private String connectString = @"Server = .;" + "Database = BicycleStores; " + "Integrated Security=true;";
        public char choice = 'N';
        public bool quit = false;

        public void MainMenu()
        {
            quit = false;
            Console.WriteLine("Options:");
            Console.WriteLine("1) Get Staff Member Info");
            Console.WriteLine("2) Get Store Location and Contact Info");
            Console.WriteLine("3) Check Customer Order Status");
            Console.WriteLine("4) Check Product Availability");
            Console.WriteLine("5) Update Customer Information");
            Console.WriteLine("6) Quit");

            var input = Convert.ToInt32(Console.ReadLine());

            while (input < 1 || input > 6)
            {
                Console.WriteLine("Invalid choice");
                input = Convert.ToInt32(Console.ReadLine());
            }

            switch (input)
            {
                case 1:
                    GetStaffMemberInfo();
                    break;
                case 2:
                    GetStoreInfo();
                    break;
                case 3:
                    GetCustomerOrderStatus();
                    break;
                case 4:
                    GetProductAvailablity();
                    break;
                case 5:
                    UpdateCustomerInfo();
                    break;
                case 6:
                    quit = true;
                    break; 
            }
        }

        void GetStaffMemberInfo()
        {
            // get first and last name from user
            Console.WriteLine("Enter FirstName: ");
            var firstNameStr = Console.ReadLine();
            Console.WriteLine("Enter LastName: ");
            var lastNameStr = Console.ReadLine();

            // build the query string
            var queryString = "SELECT FirstName, LastName, Email, Phone FROM Sales.Staff" +
                " WHERE FirstName = @inputFirstName AND LastName = @inputLastName";

            // build the command from query string and connection
            using (var connection = new SqlConnection(connectString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@inputFirstName", firstNameStr);
                command.Parameters.AddWithValue("@inputLastName", lastNameStr);

                // execute query
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();

                    if(!reader.HasRows)
                    {
                        Console.WriteLine("Staff member not found.");
                        return;
                    }

                    Console.WriteLine("Staff Info:\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"First Name: {reader["FirstName"]}\nLast Name: {reader["LastName"]}\nEmail: {reader["Email"]}\nPhone: {reader["Phone"]}");
                    }
                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                    Console.ReadKey();
                }
            }
        }

        void GetStoreInfo()
        {
            // Ask for City/State OR StoreID
            Console.WriteLine("Search by:");
            Console.WriteLine("1) City / State");
            Console.WriteLine("2) StoreID");
            var choice = Convert.ToInt32(Console.ReadLine());
            while (choice < 1 || choice > 2)
            {
                Console.WriteLine("Invalid choice");
                choice = Convert.ToInt32(Console.ReadLine());
            }

            switch (choice)
            {
                case 1: // City / State
                    {
                        Console.WriteLine("Enter City: ");
                        var cityStr = Console.ReadLine();
                        Console.WriteLine("Enter State: ");
                        var stateStr = Console.ReadLine();

                        var queryString = "SELECT StoreName, Phone, Email, Street, City, State, ZipCode" +
                            " FROM Sales.Stores WHERE City = @inputCity AND State = @inputState";

                        using (var connection = new SqlConnection(connectString))
                        {
                            var command = new SqlCommand(queryString, connection);

                            command.Parameters.AddWithValue("@inputCity", cityStr);
                            command.Parameters.AddWithValue("@inputState", stateStr);

                            // execute query
                            try
                            {
                                connection.Open();
                                var reader = command.ExecuteReader();

                                if (!reader.HasRows)
                                {
                                    Console.WriteLine("Store not found.");
                                    return;
                                }
                                Console.WriteLine("Store Info:\n");
                                while (reader.Read())
                                {
                                    Console.WriteLine($"Store Name: {reader["StoreName"]}\nPhone: {reader["Phone"]}\nEmail: {reader["Email"]}\n" +
                                        $"City: {reader["City"]}\nState: {reader["State"]}\nZip: {reader["ZipCode"]}\n");
                                }
                                // what if not found?
                                
                            }
                            catch (SqlException e)
                            {
                                Console.WriteLine("SQL Error" + e.ToString());
                            }
                        }
                    }
                    break;
                case 2: // StoreID
                    {
                        Console.WriteLine("Enter StoreID: ");
                        var storeID = Convert.ToInt32(Console.ReadLine());

                        var queryString = "SELECT StoreName, Phone, Email, Street, City, State, ZipCode" +
                            " FROM Sales.Stores WHERE StoreId = @inputStoreId";

                        using (var connection = new SqlConnection(connectString))
                        {
                            var command = new SqlCommand(queryString, connection);

                            command.Parameters.AddWithValue("@inputStoreId", storeID);
                            // execute query
                            try
                            {
                                connection.Open();
                                var reader = command.ExecuteReader();

                                if (!reader.HasRows)
                                {
                                    Console.WriteLine("Store not found.");
                                    return;
                                }

                                Console.WriteLine("Store Info:\n");
                                while (reader.Read())
                                {
                                    Console.WriteLine($"Store Name: {reader["StoreName"]}\nPhone: {reader["Phone"]}\nEmail: {reader["Email"]}\n" +
                                        $"City: {reader["City"]}\nState: {reader["State"]}\nZip: {reader["ZipCode"]}\n");
                                }

                            }
                            catch (SqlException e)
                            {
                                Console.WriteLine("SQL Error" + e.ToString());
                                Console.ReadKey();
                            }
                        }
                    }
                    break;
            }
        }

        void GetCustomerOrderStatus()
        {
            Console.WriteLine("Enter OrderID: ");
            var orderID = Convert.ToInt32(Console.ReadLine());

            var queryString = "SELECT OrderId, OrderStatus, OrderDate, RequiredDate, ShippedDate" +
                            " FROM Sales.Orders WHERE OrderId = @inputOrderId";

            using (var connection = new SqlConnection(connectString))
            {
                var command = new SqlCommand(queryString, connection);

                command.Parameters.AddWithValue("@inputOrderId", orderID);
                // execute query
                try
                {
                    connection.Open();
                    var reader = command.ExecuteReader();

                    if (!reader.HasRows)
                    {
                        Console.WriteLine("Order not found.");
                        return;
                    }

                    Console.WriteLine("Order Info:\n");
                    while (reader.Read())
                    {
                        Console.WriteLine($"OrderId: {reader["OrderId"]}\nStatus: {reader["OrderStatus"]}\nOrder Date: {reader["OrderDate"]}\n" +
                            $"Required Date: {reader["RequiredDate"]}\nShipped Date: {reader["ShippedDate"]}");
                    }
                    // what if not found?
                    

                }
                catch (SqlException e)
                {
                    Console.WriteLine("SQL Error" + e.ToString());
                }
            }

        }

        void GetProductAvailablity()
        {
            Console.WriteLine("Search by:");
            Console.WriteLine("1) Product Id");
            Console.WriteLine("2) Name");
            var choice = Convert.ToInt32(Console.ReadLine());
            while (choice < 1 || choice > 2)
            {
                Console.WriteLine("Invalid choice");
                choice = Convert.ToInt32(Console.ReadLine());
            }

            switch (choice)
            {
                case 1:// By ProductId
                    {
                        Console.WriteLine("Enter ProductId: ");
                        var inputProductId = Convert.ToInt32(Console.ReadLine());

                        var queryString = "SELECT stocks.Quantity, stores.StoreName, stores.Phone, stores.Email, stores.Street, stores.City, stores.State, stores.ZipCode" +
                            " FROM Production.Products p JOIN Production.Stocks stocks ON p.ProductId = stocks.ProductId" +
                            " JOIN Sales.Stores stores ON stores.StoreId = stocks.StoreId " +
                            " WHERE p.ProductId = @inputProductId" +
                            " GROUP BY Quantity, Stores.StoreName, stores.Phone, stores.Email, stores.Street, stores.City, stores.State, stores.ZipCode";

                        using (var connection = new SqlConnection(connectString))
                        {
                            var command = new SqlCommand(queryString, connection);

                            command.Parameters.AddWithValue("@inputProductId", inputProductId);
                            // execute query
                            try
                            {
                                connection.Open();
                                var reader = command.ExecuteReader();

                                if (!reader.HasRows)
                                {
                                    Console.WriteLine("Product not found.");
                                    return;
                                }

                                Console.WriteLine("Product Availability:\n");
                                while (reader.Read())
                                {

                                    Console.WriteLine($"Quantity: {reader["Quantity"]}\nStore Name: {reader["StoreName"]}\nPhone: {reader["Phone"]}\n" +
                                        $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                        $"Zip: {reader["ZipCode"]}");
                                    Console.WriteLine();

                                }
                            
                            }
                            catch (SqlException e)
                            {
                                Console.WriteLine("SQL Error" + e.ToString());
                            }
                        }
                    }
                    break;
                case 2:// By Product Name
                    {
                        Console.WriteLine("Enter Product Name: ");
                        var inputProductName = Console.ReadLine();

                        var queryString = "SELECT stocks.Quantity, stores.StoreName, stores.Phone, stores.Email, stores.Street, stores.City, stores.State, stores.ZipCode" +
                            " FROM Production.Products p JOIN Production.Stocks stocks ON p.ProductId = stocks.ProductId" +
                            " JOIN Sales.Stores stores ON stores.StoreId = stocks.StoreId " +
                            " WHERE p.ProductName = @inputProductName" +
                            " GROUP BY Quantity, Stores.StoreName, stores.Phone, stores.Email, stores.Street, stores.City, stores.State, stores.ZipCode";

                        using (var connection = new SqlConnection(connectString))
                        {
                            var command = new SqlCommand(queryString, connection);

                            command.Parameters.AddWithValue("@inputProductName", inputProductName);
                            // execute query
                            try
                            {
                                connection.Open();
                                var reader = command.ExecuteReader();

                                if (!reader.HasRows)
                                {
                                    Console.WriteLine("Product not found.");
                                    return;
                                }

                                Console.WriteLine("Product Availability:\n");
                                while (reader.Read())
                                {

                                    Console.WriteLine($"Quantity: {reader["Quantity"]}\nStore Name: {reader["StoreName"]}\nPhone: {reader["Phone"]}\n" +
                                        $"Email: {reader["Email"]}\nStreet: {reader["Street"]}\nCity: {reader["City"]}\nState: {reader["State"]}\n" +
                                        $"Zip: {reader["ZipCode"]}");
                                    Console.WriteLine();

                                }
                                
                            }
                            catch (SqlException e)
                            {
                                Console.WriteLine("SQL Error" + e.ToString());
                            }
                        }
                    }
                    break;
            }
        }

        void UpdateCustomerInfo()
        {
            var update = new Update();
            update.GetSearchChoice();
            switch(update.SearchBy)
            {
                case 1:
                    {
                        Console.WriteLine("Enter CustomerId: ");
                        var inputCustomerId = Convert.ToInt32(Console.ReadLine());
                        while (!update.CheckRecordById(inputCustomerId))
                        {
                            Console.WriteLine("Record not found.");
                            Console.WriteLine("Enter CustomerId: ");
                            inputCustomerId = Convert.ToInt32(Console.ReadLine());
                        }
                        //update.CustomerId.Equals(inputCustomerId);
                        Console.WriteLine();
                        update.GetUpdateFieldChoice();
                        update.UpdateFieldById(inputCustomerId);
                    }
                    break;
                case 2:
                    {
                        Console.WriteLine("Enter Customer FirstName:");
                        var inputFirstName = Console.ReadLine();
                        Console.WriteLine("Enter Customer LastName:");
                        var inputLastName = Console.ReadLine();
                        while(!update.CheckRecordByName(inputFirstName, inputLastName))
                        {
                            Console.WriteLine("Record not found.");
                            Console.WriteLine("Enter Customer FirstName:");
                            inputFirstName = Console.ReadLine();
                            Console.WriteLine("Enter Customer LastName:");
                            inputLastName = Console.ReadLine();
                        }
                        //update.FirstName.Equals(inputFirstName);
                        //update.LastName.Equals(inputLastName);
                        Console.WriteLine();
                        update.GetUpdateFieldChoice();
                        update.UpdateFieldByName(inputFirstName, inputLastName);
                    }
                    break;
            }
            
        }

    }
}
