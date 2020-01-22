using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Newtonsoft.Json;

namespace RefactorThis.Models
{
    public class Product
    {

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        [JsonIgnore] public bool IsNew { get; }

        public Product()
        {
            Id = Guid.NewGuid();
            IsNew = true;
        }

        public Product(Guid id)
        {
            IsNew = true;
            var database = new Database();
            var sql = $"select * from Products where id = '{id}' collate nocase";

            database.Query(sql);
            if (!database.reader.Read())
            {
                return;
            }

            IsNew           = false;
            Id              = Guid.Parse(database.reader["Id"].ToString());
            Name            = database.reader["Name"].ToString();
            Description     = (DBNull.Value == database.reader["Description"]) ? null : database.reader["Description"].ToString();
            Price           = decimal.Parse(database.reader["Price"].ToString());
            DeliveryPrice   = decimal.Parse(database.reader["DeliveryPrice"].ToString());
        }

        public bool Save()
        {
            var database = new Database();
            string sql;

            if (IsNew)
            {
                // Insert New Product
                sql =
                    $"INSERT INTO Products (id, name, description, price, deliveryprice) " +
                    $"values ('{Id}', '{Name}', '{Description}', '{Price}', '{DeliveryPrice}')";
            } else
            {
                // Update Product
                sql = $"UPDATE Products " +
                      $"SET name = '{Name}', description = '{Description}', price = {Price}, deliveryprice = {DeliveryPrice} WHERE id = '{Id}' collate nocase";
            }

            // Attempted to prevent SQL injection here with no solution
            try
            {
                database.Query(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }

        public bool Delete()
        {

            try
            {
                // Remove Options attached to Product before deleting
                foreach (var option in new ProductOptions(Id).Items)
                {
                    option.Delete();
                }

                var database = new Database();
                var sql = $"delete from Products where id = '{Id}' collate nocase";
                database.Query(sql);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }

            return true;
        }


    }
}
