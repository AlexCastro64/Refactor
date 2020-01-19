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

        private void LoadProducts(string where)
        {
            Items = new List<Product>();
            var database = Database();
            var sql = $"select id from Products {where}";
            var rdr = database.query(sql);
            while (rdr.Read())
            {
                var id = Guid.Parse(rdr.GetString(0));
                Items.Add(new Product(id));
            }
        }
    }

}