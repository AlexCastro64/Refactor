using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RefactorThis.Models
{   

    public class Products
    {
        public List<Product> Items { get; private set; }

        public Products()
        {
            LoadProducts(null);
        }

        public Products(string name)
        {
            LoadProducts($"where lower(name) like '%{name.ToLower()}%'");
        }

        // Load Products.
        private void LoadProducts(string where)
        {
            Items = new List<Product>();
            var database = new Database();
            var sql = $"select id from Products {where}";
            database.Query(sql);
            while (database.reader.Read())
            {
                var id = Guid.Parse(database.reader.GetString(0));
                Items.Add(new Product(id));
            }
        }
    }

}