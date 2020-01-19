using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            var database = Database();
            var sql = $"select * from Products where id = '{id}' collate nocase";

            var rdr = database.query(sql);
            if (!rdr.Read())
                return;

            IsNew           = false;
            Id              = Guid.Parse(rdr["Id"].ToString());
            Name            = rdr["Name"].ToString();
            Description     = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();
            Price           = decimal.Parse(rdr["Price"].ToString());
            DeliveryPrice   = decimal.Parse(rdr["DeliveryPrice"].ToString());
        }

        public void Save()
        {
            var database = Database();
            var sql = IsNew
                ? $"insert into Products (id, name, description, price, deliveryprice) values ('{Id}', '{Name}', '{Description}', {Price}, {DeliveryPrice})"
                : $"update Products set name = '{Name}', description = '{Description}', price = {Price}, deliveryprice = {DeliveryPrice} where id = '{Id}' collate nocase";
            database.query(sql);
        }

        public void Delete()
        {
            foreach (var option in new ProductOptions(Id).Items)
            {
                option.Delete();
            }

            var database = Database();
            var sql = $"delete from Products where id = '{Id}' collate nocase";
            database.query(sql);
        }
        
    }
}
