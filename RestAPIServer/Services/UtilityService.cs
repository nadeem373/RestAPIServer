using Newtonsoft.Json;
using RestAPIServer.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace RestAPIServer.Services
{
    public static class UtilityService
    {
        public static List<string> FirstName = new List<string>() { "Leia", "Sadie", "Jose", "Sara", "Frank", "Dewey", "Tomas", "Joel", "Lukas", "Carlos" };
        public static List<string> LastName = new List<string>() { "Liberty", "Ray", "Harrison", "Ronan", "Drew", "Powell", "Larsen", "Chan", "Anderson", "Lane" };

        public static void WriteInDataStore(List<Customer> customer)
        {
            var updatedCustomers = BuildCustomerDataIds(customer);
            string json = JsonConvert.SerializeObject(updatedCustomers);
            var filePath = GetFileDataStoreDirectoryPath();
            if (File.Exists(filePath))
            {
                //TOOD: Sort the data based on Last and First Name before saving in the file.
                File.WriteAllText(filePath, string.Empty);
                File.AppendAllText(filePath, json);
            }
        }

        private static List<Customer> BuildCustomerDataIds(List<Customer> customer)
        {
            List<Customer> dbcustomers = ReadFromDataStore();

            int latestId = 0;
            if (dbcustomers != null && dbcustomers.Count > 0)
            {
                latestId = dbcustomers.OrderByDescending(x => x.Id).Select(x => x.Id).First();
            }

            //Set Latest ids to all new customers data..
            foreach (var newCustomer in customer)
            {
                latestId += 1;
                newCustomer.Id = latestId;
                dbcustomers.Add(newCustomer);
            }
            return dbcustomers;
        }

        public static List<Customer> ReadFromDataStore()
        {
            List<Customer> customers = new List<Customer>();

            var filePath = GetFileDataStoreDirectoryPath();
            if (File.Exists(filePath))
            {
                var data = File.ReadAllText(filePath);
                if (data.Length > 0)
                    customers = JsonConvert.DeserializeObject<List<Customer>>(data);
            }
            return customers;
        }

        private static string GetFileDataStoreDirectoryPath()
        {
            var directoryPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase)?.Replace("file:\\", "");
            return Path.Combine(directoryPath, "DataStore.txt");
        }

        public static List<Customer> GenerateCustomer(int noofCustomer)
        {
            var customers = new List<Customer>();
            for (int i = 0; i < noofCustomer; i++)
            {
                Customer customer = new Customer
                {
                    FirstName = FirstName[new Random().Next(0, 9)],
                    LastName = LastName[new Random().Next(0, 9)],
                    Age = new Random().Next(10, 90)
                };
                Thread.Sleep(200);
                customers.Add(customer);
            }

            return customers;
        }
    }
}
