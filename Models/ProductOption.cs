using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

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
            var database = new Database();
            var sql = $"SELECT * " +
                            $"FROM productoptions " +
                            $"WHERE id = '{id}' collate nocase";
            database.Query(sql);

            if (!database.reader.Read())
            {
                return;
            }

            IsNew           = false;
            Id              = Guid.Parse(database.reader["Id"].ToString());
            ProductId       = Guid.Parse(database.reader["ProductId"].ToString());
            Name            = database.reader["Name"].ToString();
            Description     = (DBNull.Value == database.reader["Description"]) ? null : database.reader["Description"].ToString();

        }

        public bool Save()
        {
            try
            {
                var database = new Database();
                string sql;

                if (IsNew)
                {
                    // Insert New Option
                    sql =
                        $"INSERT INTO productoptions (id, productid, name, description) " +
                        $"VALUES ('{Id}', '{ProductId}', '{Name}', '{Description}')";
                }
                else
                {
                    // Update Option
                    sql =
                        $"UPDATE productoptions " +
                        $"SET name = '{Name}', description = '{Description}' " +
                        $"WHERE id = '{Id}' collate nocase";
                }

                database.Query(sql);
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
                return false;
            }

            return true;
        }

        public bool Delete()
        {
            try
            {
                var database = new Database();
                var sql = $"delete from productoptions where id = '@Id' collate nocase";
                database.Command.Parameters.AddWithValue("@Id", Id);
                database.Query(sql);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            return true;
        }
    }
}
