using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RefactorThis.Models
{
    public class ProductOption
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [JsonIgnore] public bool IsNew { get; }

        public ProductOption()
        {
            Id = Guid.NewGuid();
            IsNew = true;
        }

        public ProductOption(Guid id)
        {
            IsNew = true;
            var database = Database();

            var rdr = database.query($"select * from productoptions where id = '{id}' collate nocase");
            if (!rdr.Read())
                return;

            IsNew           = false;
            Id              = Guid.Parse(rdr["Id"].ToString());
            ProductId       = Guid.Parse(rdr["ProductId"].ToString());
            Name            = rdr["Name"].ToString();
            Description     = (DBNull.Value == rdr["Description"]) ? null : rdr["Description"].ToString();
        }

        public void Save()
        {
            var database = Database();

            cvar sql = IsNew
                ? $"insert into productoptions (id, productid, name, description) values ('{Id}', '{ProductId}', '{Name}', '{Description}')"
                : $"update productoptions set name = '{Name}', description = '{Description}' where id = '{Id}' collate nocase";

            database.query(sql);
        }

        public void Delete()
        {
            var database = Database();
            var sql = $"delete from productoptions where id = '{Id}' collate nocase";
            database.query(sql);
        }
    }
}
